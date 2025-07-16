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

	public async Task<IEnumerable<MovieWithGenreDto>> GetAllMoviesAsync()
	{
		var movie = await _unitOfWork.Movies.GetAllMoviesAsync(changeTracker: false);

		return _mapper.Map<IEnumerable<MovieWithGenreDto>>(movie);
	}

	public async Task<MovieWithGenreDto?> GetMovieAsync(int id)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id, changeTracker: false);

		if (movie is null)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie genre ID",
			//	detail: $"No movie genre with ID {id} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException(nameof(movie), $"No movie genre with ID {id} was found.");
		}

		return _mapper.Map<MovieWithGenreDto>(movie);
	}

	public async Task<MovieWithGenreDetailsDto?> GetMovieDetailsAsync(int id)
	{
		var movieWithGenreDetailsDto = _mapper.Map<MovieWithGenreDetailsDto>(
				await _unitOfWork.Movies.GetMovieDetailsAsync(id, changeTracker: false)
		);

		if (movieWithGenreDetailsDto is null)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie genre ID",
			//	detail: $"No movie genre with ID {id} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException(nameof(movieWithGenreDetailsDto), $"No movie genre with ID {id} was found.");
		}

		return movieWithGenreDetailsDto;
	}

	public async Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id)
	{
		var movieExists = await _unitOfWork.Movies.AnyAsync(id);

		if (!movieExists)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie ID",
			//	detail: $"No movie with ID {id} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException($"No movie with ID {id} was found.");
		}

		return await _unitOfWork.Movies.GetMovieFullDetailsAsync(id, changeTracker: false);
	}
}
