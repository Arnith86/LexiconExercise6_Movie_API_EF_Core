using Services.Contracts;
using Services.Contracts.Contracts;

namespace Movie.Services;

public class ServiceManager : IServiceManager
{
	private Lazy<IMovieServices> _movieServices;
	public IMovieServices MovieServices => _movieServices.Value;

	public ServiceManager(Lazy<IMovieServices> movieServices)
	{
		_movieServices = movieServices;
	}
}
