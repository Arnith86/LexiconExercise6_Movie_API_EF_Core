using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
	public ActorRepository(MovieApiContext context) : base(context)
	{
	}

	public Task<bool> AnyAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Actor>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Actor> GetAsync(int id)
	{
		throw new NotImplementedException();
	}
}
