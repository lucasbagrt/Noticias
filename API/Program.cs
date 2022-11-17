using API.Entities.Utils;
using API.Infra;
using API.Infra.Utils;
using API.Mappers;
using API.Middleware;
using API.Services;
using API.Services.Cache;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<TokenManagement>(builder.Configuration.GetSection("tokenManagement"));

#region [Database]
services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
#endregion

#region [Cache]
services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
});
#endregion

#region [HealthCheck]
services.AddHealthChecks()
             .AddRedis(builder.Configuration.GetSection("Redis:ConnectionString").Value, tags: new string[] { "db", "data" })
             .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value + "/" + builder.Configuration.GetSection("DatabaseSettings:db_portal").Value,
                    name: "mongodb", tags: new string[] { "db", "data" });


services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.SetApiMaxActiveRequests(1);

    opt.AddHealthCheckEndpoint("default api", "/health");
}).AddInMemoryStorage();
#endregion

#region [DI]
services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
services.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
services.AddSingleton<NewsService>();
services.AddSingleton<VideoService>();

services.AddTransient<UploadService>();
services.AddTransient<TokenService>();
services.AddTransient<UserService>();
services.AddTransient<GalleryService>();

services.AddSingleton<ICacheService, CacheRedisService>();
#endregion

#region [AutoMapper]
services.AddAutoMapper(typeof(MappingConfig));
#endregion

#region [Cors]
services.AddCors();
#endregion

#region [JWT]
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(builder.Configuration.GetSection("tokenManagement:secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

#region [Swagger]
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Artigos v1", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
      {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
       }
    });
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Artigos v1"));
}

#region [Healthcheck]
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

}).UseHealthChecksUI(h => h.UIPath = "/healthui");
#endregion

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
#endregion

#region [StaticFiles]
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Medias")),
    RequestPath = "/medias"
});
#endregion

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();