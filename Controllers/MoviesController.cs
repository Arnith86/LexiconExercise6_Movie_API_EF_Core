using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs.MovieDtos;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using System.ComponentModel;

namespace MovieApi.Controllers;

[Route("api/movie")]
[ApiController]
public class MoviesController : ControllerBase
{
	private readonly MovieApiContext _context;

	public MoviesController(MovieApiContext context)
	{
		_context = context;
	}


	// GET: api/Movies
	/// <summary>
	/// Retrieves all registered movies with basic details and their associated genre.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a simplified list of movies. Each movie includes:
	/// Id, Title, Genre (as a string), Duration, Release Year
	/// </remarks>
	/// <returns>A list of movies with basic information and genre.</returns>
	/// <response code="200">Returns the list of movies successfully.</response>
	[HttpGet]
	[SwaggerOperation(
		Summary = "Retrieve all movies",
		Description = "Returns a simplified list of all registered movies including basic details and genre.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieWithGenreDto>))]
	public async Task<ActionResult<IEnumerable<MovieWithGenreDto>>> GetMovies()
	{
		// Todo: use automapper
		List<MovieWithGenreDto> movieWithGenreDtos = await _context.Movies.
			Include(mg => mg.MoviesGenre)
			.Select(mwg => new MovieWithGenreDto
			{
				Id = mwg.Id,
				Duration = mwg.Duration,
				Year = mwg.Year,
				Title = mwg.Title,
				Genre = mwg.MoviesGenre!.Genre
			})
			.ToListAsync();

		return Ok(movieWithGenreDtos);
	}

	// GET: api/Movies/5
	/// <summary>
	/// Retrieves a simplified representation of a specific movie by its ID.
	/// </summary>
	/// <param name="id">The unique identifier of the movie.</param>
	/// <remarks>
	/// Returns basic movie details including:
	/// Id, Title, Genre (as a string), Duration, Release Year
	/// </remarks>
	/// <returns>The requested movie if found; otherwise, a 404 Not Found response.</returns>
	/// <response code="200">Returns the requested movie.</response>
	/// <response code="404">If no movie with the specified ID exists.</response>
	[HttpGet("{id}")]
	[SwaggerOperation(
		Summary = "Get a specific movie by ID",
		Description = "Returns simplified movie details for the movie with the given ID, including genre info.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieWithGenreDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<MovieWithGenreDto>> GetMovie(int id)
	{
		// Todo: use automapper
		var movieWithGenreDto = await _context.Movies
			.Include(mwg => mwg.MoviesGenre)
			.Select(mwg => new MovieWithGenreDto
			{
				Id = mwg.Id,
				Duration = mwg.Duration,
				Year = mwg.Year,
				Title = mwg.Title,
				Genre = mwg.MoviesGenre!.Genre
			})
			.FirstOrDefaultAsync(mwg => mwg.Id == id);

		if (movieWithGenreDto is null) return NotFound();

		return Ok(movieWithGenreDto);
	}

	// GET: api/Movies/5/details
	/// <summary>
	/// Retrieves a simplified representation of a specific movie by its ID, with more detailed information.
	/// </summary>
	/// <param name="id">The unique identifier of the movie.</param>
	/// <remarks>
	/// Returns movie details including:
	/// - Id, Title, Genre (as a string), Duration, Release Year
	/// - Synopsis, Language and Budget
	/// </remarks>
	/// <returns>The requested movie if found; otherwise, a 404 Not Found response.</returns>
	/// <response code="200">Returns the requested movie.</response>
	/// <response code="404">If no movie with the specified ID exists.</response>
	[HttpGet("{id}/details")]
	[SwaggerOperation(
		Summary = "Get a specific movie by ID",
		Description = "Returns movie details for the movie with the given ID, including genre info and details.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieWithGenreDetailsDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<MovieWithGenreDetailsDto>> GetMovieDetails(int id)
	{
		// Todo: use automapper
		var movieWithGenreDetailsDto = await _context.Movies
			.Include(mwgd => mwgd.MoviesDetails)
			.Include(mwgd => mwgd.MoviesGenre)
			.Select(mwgd => new MovieWithGenreDetailsDto
			{
				Id = mwgd.Id,
				Duration = mwgd.Duration,
				Year = mwgd.Year,
				Title = mwgd.Title,
				Genre = mwgd.MoviesGenre!.Genre,
				Synopsis = mwgd.MoviesDetails!.Synopsis,
				Language = mwgd.MoviesDetails!.Language,
				Budget = mwgd.MoviesDetails.Budget
			})
			.FirstOrDefaultAsync(mbwgdto => mbwgdto.Id == id);

		if (movieWithGenreDetailsDto is null) return NotFound();

		return Ok(movieWithGenreDetailsDto);
	}


	// POST: api/Movies
	/// <summary>
	/// Creates a new movie with basic information such as title, year, duration, and genre.
	/// </summary>
	/// <param name="genreId">The ID of the genre to associate with the new movie. Must refer to an existing genre.</param>
	/// <param name="movieCreateDto">The data used to create the movie.</param>
	/// <returns>The created movie's basic information including the associated genre ID.</returns>
	/// <response code="201">Returns the created movie with genre ID.</response>
	/// <response code="400">Returned if the specified genre does not exist or the request is invalid.</response>
	[HttpPost]
	[SwaggerOperation(
		Summary = "Create a new movie.",
		Description = "Adds a new movie to the database using basic details like title, year, " +
		"and duration. Requires a valid genre ID to associate the movie with."
	)]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieWithGenreIdDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
	[HttpPost]
	public async Task<ActionResult<MovieWithGenreIdDto>> PostMovie(MovieCreateDto movieCreateDto)
	{

		var genre = await _context.MovieGenres.FirstOrDefaultAsync(g => g.Id == movieCreateDto.MovieGenreId);

		// Todo: will this send readable error to user?
		if (genre is null) return BadRequest($"No genre with ID {movieCreateDto.MovieGenreId} was found.");

		// use automapper
		Movie movie = new Movie() 
		{
			Title = movieCreateDto.Title,
			Year = movieCreateDto.Year,
			Duration = movieCreateDto.Duration,
			MovieGenreId = movieCreateDto.MovieGenreId
		};

		_context.Movies.Add(movie);
		await _context.SaveChangesAsync();

		// use automapper
		MovieWithGenreIdDto movieWithGenreDto = new MovieWithGenreIdDto()
		{
			Id = movie.Id,
			Title = movie.Title,
			Year = movie.Year,
			Duration = movie.Duration,
			MovieGenreId = movie.MovieGenreId
		};

		return CreatedAtAction("GetMovie", new { id = movie.Id }, movieWithGenreDto);
	}

	// PUT: api/Movies/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutMovie(int id, Movie movie)
	{
		if (id != movie.Id)
		{
			return BadRequest();
		}

		_context.Entry(movie).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!MovieExists(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}


	// DELETE: api/Movies/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMovie(int id)
	{
		var movie = await _context.Movies.FindAsync(id);
		if (movie == null)
		{
			return NotFound();
		}

		_context.Movies.Remove(movie);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool MovieExists(int id)
	{
		return _context.Movies.Any(e => e.Id == id);
	}
}
