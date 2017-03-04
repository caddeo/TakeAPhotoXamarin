using System;

namespace ImagesWebApi.Models
{
    public class SavedImage
    {
        public Guid Id { get; set; }
        public string ImageConverted { get; set; }
        public string Text { get; set; }
    }
}
