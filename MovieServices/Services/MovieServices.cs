using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;
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

			throw new ArgumentNullException($"No movie with ID {id} was found.");
		}

		return await _unitOfWork.Movies.GetMovieFullDetailsAsync(id, changeTracker: false);
	}

	public async Task<(MovieWithGenreIdDto? mwgiDto, int movieId)> AddMovieAsync(MovieCreateDto movieCreateDto)
	{
		var genre = await _unitOfWork.MovieGenres.AnyAsync(movieCreateDto.MovieGenreId);

		if (!genre)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status400BadRequest,
			//	title: "Invalid movie genre ID",
			//	detail: $"No movie genre with ID {movieCreateDto.MovieGenreId} was found.",
			//	instance: HttpContext.Request.Path
			//);

			throw new ArgumentNullException(
				nameof(movieCreateDto), 
				$"No movie genre with ID {movieCreateDto.MovieGenreId} was found."
			);
		}

		VideoMovie movie = _mapper.Map<VideoMovie>(movieCreateDto);

		_unitOfWork.Movies.Add(movie);
		await _unitOfWork.CompleteAsync();

		return (_mapper.Map<MovieWithGenreIdDto>(movie), movie.Id);
	}

	public async Task<bool> UpdateMovieAsync(int id, MovieWithGenreIdUpdateDto movieWithGenreIdUpdateDto)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id, changeTracker: true);

		if (movie is null)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie ID",
			//	detail: $"No movie with ID {id} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException(nameof(movie), $"No movie with ID {id} was found.");
		}

		var genre = await _unitOfWork.MovieGenres.AnyAsync(movieWithGenreIdUpdateDto.MovieGenreId);

		if (!genre)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status400BadRequest,
			//	title: "Invalid movie genre ID",
			//	detail: $"No movie genre with ID {movieWithGenreIdUpdateDto.MovieGenreId} was found.",
			//	instance: HttpContext.Request.Path
			//);

			throw new ArgumentNullException(
				$"No movie genre with ID {movieWithGenreIdUpdateDto.MovieGenreId} was found."
			);
		}

		_mapper.Map(movieWithGenreIdUpdateDto, movie);

		try
		{
			await _unitOfWork.CompleteAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await _unitOfWork.Movies.AnyAsync(id))
			{
				// ToDo : Create a custom exception and handle this exception in program.cs //NotFound();
				throw new ArgumentNullException(
					$"No movie genre with ID {movieWithGenreIdUpdateDto.MovieGenreId} was found."
				);
			}
			else
			{
				throw;
			}
		}

		return true;
	}
}
