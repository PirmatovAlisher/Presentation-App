using System.ComponentModel.DataAnnotations.Schema;

namespace PresentationApp.Models
{
    public class TextBlock
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; } = "**New Text**";
        public double Top { get; set; } = 100;
        public double Left { get; set; } = 100;
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 100;

        [NotMapped]
        public string DraftContent { get; set; }
        [NotMapped]
        public bool IsEditing { get; set; }
    }
}
