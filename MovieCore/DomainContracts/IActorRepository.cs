using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

public interface IActorRepository : IRepositoryBase<Actor>
{
	public Task<bool> AnyAsync(int id);
	public Task<IEnumerable<Actor>> GetAllAsync();
	public Task<Actor?> GetAsync(int id);
	void Add(Actor entity);
	void update(Actor entity);
	void remove(Actor entity);
}
