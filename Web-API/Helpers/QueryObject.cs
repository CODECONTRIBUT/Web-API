namespace Web_API.Helpers
{
    public class QueryObject
    {
        public int? genres { get; set; } = null;

        public int? platforms { get; set; } = null;

        public string? search { get; set; } = null;

        public string? ordering { get; set; }

        public int page { get; set; } = 1;

        public int page_size { get; set; } = 12;
    }
}
