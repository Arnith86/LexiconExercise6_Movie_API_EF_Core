using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieDtos;
using Services.Contracts.Contracts;
using System.Net.Http;

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

	public async Task<MovieWithGenreDto?> GetMovieAsync(int id, bool changeTracker = false)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id, changeTracker);

		if (movie is null)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie genre ID",
			//	detail: $"No movie genre with ID {id} was found.",
			//	instance: HttpContext.Request.Path
			//);

			throw new ArgumentNullException(nameof(movie));  // ToDo : Handle this exception in program.cs 
		}

		return _mapper.Map<MovieWithGenreDto>(movie);
	}
}
