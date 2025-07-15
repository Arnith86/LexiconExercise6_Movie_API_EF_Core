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

	public async Task<bool> AnyAsync(int id) => await FindAnyAsync(m => m.Id.Equals(id));

	public async Task<List<Movie>> GetAllMoviesAsync(bool changeTracker = false) => 
		await GetAll(changeTracker).Include(m => m.MoviesGenre).ToListAsync();

	public async Task<Movie?> GetMovieAsync(int id, bool changeTracker = false) =>	
		await GetByCondition(m => m.Id.Equals(id), changeTracker)
				.Include(m => m.MoviesGenre)
				.FirstOrDefaultAsync();

	public async Task<Movie?> GetMovieDetailsAsync(int id, bool changeTracker = false) =>
		await GetByCondition(gmd => gmd.Id.Equals(id))
				.Include(md => md.MoviesDetails)
				.Include(mg => mg.MoviesGenre)
				.FirstOrDefaultAsync();

	//public async Task<Movie?> GetMovieFullDetailsAsync(int movieId, bool changeTracker = false) => // FINISH THIS 
	//	await GetByCondition()
	//		.Include(r => r.Reviews)
	//		.Include(md => md.MoviesDetails)
	//		.Include(mg => mg.MoviesGenre)
	//		.Include(a => a.MovieActors)
	//		.FirstOrDefaultAsync();

	
}
