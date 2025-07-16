using MovieCore.Models.DTOs.MovieDtos;

namespace Services.Contracts.Contracts;

public interface IMovieServices
{
	Task<IEnumerable<MovieWithGenreDto?>> GetAllMoviesAsync(bool changeTracker = false);
	Task<MovieWithGenreDto?> GetMovieAsync(int id, bool changeTracker = false);
}
