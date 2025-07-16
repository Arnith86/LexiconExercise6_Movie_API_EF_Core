using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieDtos;
using Services.Contracts.Contracts;

namespace Movie.Services.Services;

public class MovieServices : IMovieServices
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public MovieServices(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<MovieWithGenreDto?>> GetAllMoviesAsync(bool changeTracker = false)
	{
		var movie = await _unitOfWork.Movies.GetAllMoviesAsync(changeTracker);

		return _mapper.Map<IEnumerable<MovieWithGenreDto>>(movie);
	}	
}
