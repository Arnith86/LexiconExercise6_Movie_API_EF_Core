using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
	public ReviewRepository(MovieApiContext context) : base(context)
	{
	}

	public Task<bool> AnyAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Review>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Review> GetAsync(int id)
	{
		throw new NotImplementedException();
	}
}
