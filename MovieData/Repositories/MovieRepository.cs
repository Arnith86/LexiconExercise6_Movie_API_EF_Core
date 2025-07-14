using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

internal class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{

	public MovieRepository(MovieApiContext context) : base(context)
	{
	}

	public Task<bool> AnyAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Movie>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Movie> GetAsync(int id)
	{
		throw new NotImplementedException();
	}
}
