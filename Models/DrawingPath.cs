namespace PresentationApp.Models
{
    public class DrawingPath
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Color { get; set; } = "#000000";
        public int LineWidth { get; set; } = 3;
        public List<Point> Points { get; set; } = new();
    }
}
