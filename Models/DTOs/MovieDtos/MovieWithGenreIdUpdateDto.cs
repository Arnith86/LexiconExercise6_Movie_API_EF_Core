﻿using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs.MovieDtos;

public class MovieWithGenreIdUpdateDto
{
	[MaxLength(100)]
	[Required(ErrorMessage = "The movie must have a name.")]
	public string Title { get; set; } = null!;

	[Range(1850, 9999)]
	public int Year { get; set; }

	[Range(5, 300)]
	public int Duration { get; set; }

	[Required(ErrorMessage = "A movie must have a genre.")]
	public int MovieGenreId { get; set; }
}