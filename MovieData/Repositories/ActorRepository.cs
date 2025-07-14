using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
	public ActorRepository(MovieApiContext context) : base(context)
	{
	}


	public async Task<bool> AnyAsync(int id) => await _context.Actors.AnyAsync(m => m.Id.Equals(id));

	public async Task<IEnumerable<Actor>> GetAllAsync() => await _context.Actors.ToListAsync();

	public async Task<Actor?> GetAsync(int id) => await _context.Actors.FirstOrDefaultAsync(m => m.Id.Equals(id));

	public void Add(Actor entity) => _context.Actors.Add(entity);

	public void remove(Actor entity) => _context.Actors.Remove(entity);

	public void update(Actor entity) => _context.Actors.Remove(entity);
}
