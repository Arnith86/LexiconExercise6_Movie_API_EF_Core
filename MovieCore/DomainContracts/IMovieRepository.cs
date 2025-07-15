using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;


namespace MovieCore.DomainContracts;

/// <summary>
/// Defines data access operations specific to <see cref="Movie"/> entities,
/// including CRUD functionality and custom query methods.
/// Inherits generic query and modification capabilities from 
/// <see cref="IRepositoryQueries{Movie}"/> and <see cref="IRepositoryActions{Movie}"/>.
/// </summary>
public interface IMovieRepository : IRepositoryQueries<Movie>, IRepositoryActions<Movie>
{
	Task<List<Movie>> GetAllMoviesAsync(bool changeTracker = false);
	Task<Movie?> GetMovieAsync(int id, bool changeTracker = false);
	Task<bool> AnyAsync(int id);
	Task<Movie?> GetMovieDetailsAsync(int id, bool changeTracker = false);
	Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id, bool changeTracker = false);
}
