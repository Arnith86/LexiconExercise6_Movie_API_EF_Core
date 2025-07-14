using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

public interface IMovieRepository : IRepositoryBase<Movie>
{
	Task<IEnumerable<Movie>> GetAllAsync();
	Task<Movie?> GetAsync(int id);
	Task<bool> AnyAsync(int id);
	void Add(Movie entity);
	void Update(Movie entity);
	void Remove(Movie entity);
}
