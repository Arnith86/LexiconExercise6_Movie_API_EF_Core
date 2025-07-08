using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Entities;

/// <summary>
/// Represents detailed information about a movie.
/// This entity has a one-to-one relationship with the <see cref="Movie"/> entity.
/// </summary>
public class MovieDetails
{

	//[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-Increment for primary key - still needed?
	public int Id { get; set; }
	public int? MovieId { get; set; }
	public string Synopsis { get; set; } = null!;
	public string Language { get; set; } = null!;

	/// <summary>
	/// Gets or sets the Budget of the movie.
	/// The Budget value must be between 0 and 500,000,000$.
	/// </summary>
	[Range(0, 500000000)]
	public int Budget { get; set; }
	public Movie Movie { get; set; } = null!;

}
