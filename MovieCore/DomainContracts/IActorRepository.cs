using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

public interface IActorRepository : IRepositoryBase<Actor>
{

	public Task<bool> AnyAsync(int id);

	public Task<IEnumerable<Actor>> GetAllAsync();

	public Task<Actor> GetAsync(int id);
}
