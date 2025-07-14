using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

public interface IReviewRepository : IRepositoryBase<Review>
{
	public Task<bool> AnyAsync(int id);

	public Task<IEnumerable<Review>> GetAllAsync();

	public Task<Review> GetAsync(int id);
}
