using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        [Required]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Background_Image { get; set; } = string.Empty;

        [Range(0, 100)]
        public int? MetaCritic { get; set; }

        public int? Rating_Top { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }

        public int? StoreId { get; set; }

        public int? TrailerId { get; set; }

    }
}
