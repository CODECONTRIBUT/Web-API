﻿namespace Web_API.Dtos.Screenshot
{
    public class ScreenshotDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Image { get; set; }

        public DateTime? CreatedDatetime { get; set; } = DateTime.Now;
    }
}
