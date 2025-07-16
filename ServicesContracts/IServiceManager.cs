using Services.Contracts.Contracts;
using ServicesContracts.Contracts;

namespace Services.Contracts;

public interface IServiceManager
{
	IMoviesServices MovieServices { get; }
	IReviewServices ReviewServices { get; }
}
