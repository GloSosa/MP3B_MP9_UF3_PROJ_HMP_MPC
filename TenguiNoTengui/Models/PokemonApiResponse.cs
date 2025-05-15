namespace TenguiNoTengui.Models
{
    public class PokemonApiResponse
    {
        public List<PokemonCard> Data { get; set; }
    }

    public class PokemonCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PokemonImage Images { get; set; }
    }

    public class PokemonImage
    {
        public string Small { get; set; }
        public string Large { get; set; }
    }

    // ✅ AÑADE ESTA CLASE AL FINAL DEL ARCHIVO
    public class SingleCardApiResponse
    {
        public PokemonCard Data { get; set; }
    }
}
