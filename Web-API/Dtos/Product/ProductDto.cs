using Web_API.Dtos.Platform;
using Web_API.Models;

namespace Web_API.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Background_Image { get; set; } = string.Empty;

        //public List<PlatformDto> Parent_Platforms { get; set; } = new List<PlatformDto>();

        public int? MetaCritic { get; set; }

        public int? Rating_Top { get; set; }

        public string Description { get; set; } = string.Empty;

    }
}
