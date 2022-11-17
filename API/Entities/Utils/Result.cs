namespace API.Entities.Utils
{
    public class Result<T>
    {
        public int Page { get; set; }
        public int Qtd { get; set; }
        public long Total { get; set; }
        public long TotalPages { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
