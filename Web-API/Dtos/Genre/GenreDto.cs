namespace Web_API.Dtos.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? GamesCount { get; set; }

        public string ImageBackground { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
