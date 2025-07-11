namespace MovieCore.Models.Entities;

/// <summary>
/// Represents an actor entity with basic information such as name, birth year,
/// and the collection of movies the actor has acted in.
/// </summary>
public class Actor
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public int BirthYear { get; set; }

	public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}
