namespace Web_API.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Background_Image { get; set; } = string.Empty;

        //public List<PlatformDto> Parent_Platforms { get; set; } = new List<PlatformDto>();

        public int? MetaCritic { get; set; }

        public int? Rating_Top { get; set; }

        public string Description { get; set; } = string.Empty;

        public int GenreId { get; set; }

        public int? StoreId { get; set; }

        public int? PlatformId { get; set; }

        public int? ScreenshotId { get; set; }

        public int? TrailerId { get; set; }
    }
}
