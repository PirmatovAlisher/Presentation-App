namespace PresentationApp.Models
{
	public class PresentationUser
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string PresentationId { get; set; }
		public string UserName { get; set; }
		public bool IsEditor { get; set; }
	}
}
