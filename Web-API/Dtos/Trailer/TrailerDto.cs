namespace Web_API.Dtos.Trailer
{
    public class TrailerDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Preview { get; set; } = string.Empty;

        public string _480 { get; set; }

        public string Max { get; set; }
    }
}
