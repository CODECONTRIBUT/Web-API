namespace Web_API.Dtos.Genre
{
    public class UpdateGenreDto
    {
        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int? GamesCount { get; set; }

        public string ImageBackground { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
