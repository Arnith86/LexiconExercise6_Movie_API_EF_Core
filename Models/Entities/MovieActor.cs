namespace MovieApi.Models.Entities;

/// <summary>
/// Represents the many-to-many relationship between movies and actors,
/// including the role played by the actor in the specific movie.
/// </summary>
public class MovieActor
{
	public int MovieId { get; set; }
	public int ActorID { get; set; }
	public string Role { get; set; } = null!;
}
