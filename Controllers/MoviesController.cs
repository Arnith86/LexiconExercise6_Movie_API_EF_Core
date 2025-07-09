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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieBaseWithGenreDto>))]
	public async Task<ActionResult<IEnumerable<MovieBaseWithGenreDto>>> GetMovies()
	{
		// Todo: use automapper
		List<MovieBaseWithGenreDto> movieBaseWithGenreDtos = await _context.Movies.
			Include(mg => mg.MoviesGenre)
			.Select(mbwgdto => new MovieBaseWithGenreDto
			{
				Id = mbwgdto.Id,
				Duration = mbwgdto.Duration,
				Year = mbwgdto.Year,
				Title = mbwgdto.Title,
				Genre = mbwgdto.MoviesGenre!.Genre
			})
			.ToListAsync();

		return Ok(movieBaseWithGenreDtos);
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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieBaseWithGenreDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<MovieBaseWithGenreDto>> GetMovie(int id)
	{
		// Todo: use automapper
		var movieBaseWithGenreDto = await _context.Movies
			.Include(mbwgdto => mbwgdto.MoviesGenre)
			.Select(mbwgdto => new MovieBaseWithGenreDto
			{
				Id = mbwgdto.Id,
				Duration = mbwgdto.Duration,
				Year = mbwgdto.Year,
				Title = mbwgdto.Title,
				Genre = mbwgdto.MoviesGenre!.Genre
			})
			.FirstOrDefaultAsync(mbwgdto => mbwgdto.Id == id);

		if (movieBaseWithGenreDto is null) return NotFound();

		return Ok(movieBaseWithGenreDto);
	}

	// PUT: api/Movies/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

	// POST: api/Movies
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Movie>> PostMovie(Movie movie)
	{
		_context.Movies.Add(movie);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
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
