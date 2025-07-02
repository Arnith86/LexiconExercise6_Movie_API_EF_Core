using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Entities;


/// <summary>
/// Represents the link between a movie and one of its genres.
/// Each combination of MovieId and Genre is unique.
/// </summary>
[Index(nameof(MovieId), nameof(Genre), IsUnique = true)]
public class MovieGenre
{
	/// <summary>
	/// Foreign key referencing the related movie.
	/// </summary>
	public int MovieId { get; set; }

	/// <summary>
	/// Genre of the movie. Maximum length is 50 characters.
	/// </summary>
	[MaxLength(50)]
	public string Genre { get; set; } = null!;

	/// <summary>
	/// Navigation property to the related movie.
	/// </summary>
	[ForeignKey(nameof(MovieId))]
	public Movie Movie { get; set; } = null!;
}
