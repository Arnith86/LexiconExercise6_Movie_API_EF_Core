namespace MovieApi.Models.Entities;

public class MovieDetails
{
	public int Id { get; set; }
	public string Synopsis { get; set; } = null!;
	public string Language { get; set; } = null!;
	public int budget { get; set; }
}
