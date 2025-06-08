using System.ComponentModel.DataAnnotations;

namespace PresentationApp.Models
{
	public class Presentation
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Name { get; set; } = "New Presentation";
		public string CreatorName { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public List<Slide> Slides { get; set; } = new();
	}
}
