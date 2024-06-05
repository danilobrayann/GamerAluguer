namespace Api.Models
{
    public class Games
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Genero { get; set; }
        public double Valor { get; set; }
        public double ValorAluguel { get; set; }
        public string TipoDeMidia { get; set; }
    }
}
