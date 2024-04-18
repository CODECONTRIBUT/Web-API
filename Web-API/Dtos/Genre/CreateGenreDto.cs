using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos.Genre
{
    public class CreateGenreDto
    {
        [Required]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public int? GamesCount { get; set; }

        [Required]
        public string ImageBackground { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }
}
