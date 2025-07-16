using MovieCore.Models.DTOs.ReviewDTOs;

namespace ServicesContracts.Contracts;

public interface IReviewServices
{
	Task<IEnumerable<ReviewDto>> GetAllReviews(int movieId);
}
