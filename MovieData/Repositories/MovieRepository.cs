using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

internal class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{

	public MovieRepository(MovieApiContext context) : base(context)
	{
	}

	public async Task<bool> AnyAsync(int id) => await _context.Movies.AnyAsync(m => m.Id.Equals(id));

	public async Task<IEnumerable<Movie>> GetAllAsync() => await _context.Movies.ToListAsync();

	public async Task<Movie?> GetAsync(int id) => await _context.Movies.FirstOrDefaultAsync(m => m.Id.Equals(id));

	public void Add(Movie entity) => _context.Movies.Add(entity);

	public void Remove(Movie entity) => _context.Movies.Remove(entity);

	public void Update(Movie entity) => _context.Movies.Update(entity);
}
