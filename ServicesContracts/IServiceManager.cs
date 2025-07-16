using Services.Contracts.Contracts;

namespace Services.Contracts;

public interface IServiceManager
{
	IMovieServices MovieServices { get; }
}
