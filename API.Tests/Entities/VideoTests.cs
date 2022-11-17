using API.Entities;
using API.Entities.Utils;
using Xunit;

namespace API.Tests.Entities
{
    public class VideoTests
    {
        [Fact]
        public void News_Validate_Title_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Entretenimento",
                 "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.\n\nNo segundo desafio, as gêmeas nadadoras Bia e Branca Feres encaram os gêmeos lutadores Rodrigo Minotauro e Rogério Minotouro. Os irmãos terão de preparar receitas natalinas de família.\n\n",
                 "Da Redação",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("O titulo deve ter ate 90 caracteres", result.Message);
        }


        [Fact]
        public void News_Validate_Hat_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                 "Da Redação",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("O chapeu deve ter ate 40 caracteres", result.Message);
        }


        [Fact]
        public void News_Validate_Title_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Entretenimento",
                 string.Empty,
                 "Da Redação",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("O titulo nao pode estar vazio", result.Message);
        }


        [Fact]
        public void News_Validate_Hat_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 string.Empty,
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                 "Da Redação",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:7287/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("O chapeu nao pode estar vazio", result.Message);
        }
    }
}
