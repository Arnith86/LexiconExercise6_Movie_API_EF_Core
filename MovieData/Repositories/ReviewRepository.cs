using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
	public ReviewRepository(MovieApiContext context) : base(context)
	{
	}


	public async Task<bool> AnyAsync(int id) => await _context.Reviews.AnyAsync(m => m.Id.Equals(id));

	public async Task<IEnumerable<Review>> GetAllAsync() => await _context.Reviews.ToListAsync();

	public async Task<Review?> GetAsync(int id) => await _context.Reviews.FirstOrDefaultAsync(m => m.Id.Equals(id));

	public void Add(Review entity) => _context.Reviews.Add(entity);

	public void Remove(Review entity) => _context.Reviews.Remove(entity);

	public void Update(Review entity) => _context.Reviews.Update(entity);
}
