namespace MovieApi.Models.DTOs.MovieDtos
{
	public class MovieBaseWithGenreDto : MovieBaseDto
	{
		public string Genre { get; set; } = null!;
	}
}
