using MovieCore.Models.DTOs.MovieDtos;

namespace Services.Contracts.Contracts;

public interface IMovieServices
{
	Task<IEnumerable<MovieWithGenreDto>> GetAllMoviesAsync();
	Task<MovieWithGenreDto?> GetMovieAsync(int id);
	Task<MovieWithGenreDetailsDto?> GetMovieDetailsAsync(int id);
	Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id);
}
