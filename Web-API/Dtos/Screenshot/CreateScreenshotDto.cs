using System.ComponentModel.DataAnnotations;

namespace Web_API.Dtos.Screenshot
{
    public class CreateScreenshotDto
    {
        [Required]
        public string Image { get; set; }

        public DateTime? CreatedDatetime { get; set; } = DateTime.Now;
    }
}
