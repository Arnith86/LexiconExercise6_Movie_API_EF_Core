using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Entities;


/// <summary>
/// Represents the link between a movie and one of its genres.
/// Each combination of MovieId and Genre is unique.
/// </summary>
//[Index(nameof(MovieId), nameof(Genre), IsUnique = true)]
public class MovieGenre
{
	public int Id { get; set; }
	[MaxLength(50)]
	public string Genre { get; set; } = null!;
	public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
