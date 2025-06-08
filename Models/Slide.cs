﻿namespace PresentationApp.Models
{
	public class Slide
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public int Order { get; set; }
		public List<DrawingPath> Drawings { get; set; } = new();
		public List<TextBlock> TextBlocks { get; set; } = new();
	}
}
