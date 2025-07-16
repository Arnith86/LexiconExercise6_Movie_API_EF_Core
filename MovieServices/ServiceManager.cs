using Services.Contracts;
using Services.Contracts.Contracts;
using ServicesContracts.Contracts;

namespace Movie.Services;

public class ServiceManager : IServiceManager
{
	private Lazy<IMoviesServices> _movieServices;
	private Lazy<IReviewServices> _reviewServices;

	public IMoviesServices MovieServices => _movieServices.Value;
	public IReviewServices ReviewServices => _reviewServices.Value;

	public ServiceManager(
		Lazy<IMoviesServices> movieServices,
		Lazy<IReviewServices> reviewServices)
	{
		_movieServices = movieServices;
		_reviewServices = reviewServices;
	}
}
