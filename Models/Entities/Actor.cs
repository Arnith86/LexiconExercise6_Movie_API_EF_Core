using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Entities;

public class Actor
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public int BirthYear { get; set; }
}
