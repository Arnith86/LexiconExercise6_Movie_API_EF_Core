namespace MovieApi.Models.Entities;

public class Movie
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public int Year { get; set; }
	public string Genre { get; set; } = null!;
	public TimeSpan Duration { get; set; }
}
