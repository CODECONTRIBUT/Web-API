namespace Web_API.Dtos.Store
{
    public class StoreDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public int? GamesCount { get; set; }

        public string ImageBackground { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
