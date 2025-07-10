using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
	[Route("api/movie/{movieId}/actors")]
	[ApiController]
	public class ActorsController : ControllerBase
	{
		private readonly MovieApiContext _context;

		public ActorsController(MovieApiContext context)
		{
			_context = context;
		}

		//// GET: api/Actors
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
		//{
		//	return await _context.Actors.ToListAsync();
		//}

		//// GET: api/Actors/5
		//[HttpGet("{id}")]
		//public async Task<ActionResult<Actor>> GetActor(int id)
		//{
		//	var actor = await _context.Actors.FindAsync(id);

		//	if (actor == null)
		//	{
		//		return NotFound();
		//	}

		//	return actor;
		//}

		//// PUT: api/Actors/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutActor(int id, Actor actor)
		//{
		//	if (id != actor.Id)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Entry(actor).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!ActorExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		//// POST: api/Actors
		//[HttpPost()]
		//public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto actorCreateDto)
		//{
		//	Actor actor = new Actor()
		//	{
		//		Name = actorCreateDto.Name,
		//		BirthYear = actorCreateDto.BirthYear,
		//		Movies = actorCreateDto.Movies as ICollection<Movie>
		//	};

		//	_context.Actors.Add(actor);
		//	await _context.SaveChangesAsync();

		//	return CreatedAtAction("GetActor", new { id = actor.Id }, actorCreateDto);
		//}

		// POST /api/movies/{movieId}/actors/{actorId}
		/// <summary>
		/// Associates an existing actor with an existing movie.
		/// </summary>
		/// <param name="movieId">The ID of the movie to associate the actor with.</param>
		/// <param name="actorId">The ID of the actor to associate with the movie.</param>
		/// <returns>No content on success; NotFound if the actor or movie is not found.</returns>
		/// <response code="204">The actor was successfully associated with the movie.</response>
		/// <response code="404">The movie or actor with the specified ID was not found.</response>
		[HttpPost("{actorId}")]
		[SwaggerOperation(
			Summary = "Associate an actor with a movie.",
			Description = "Links an existing actor to an existing movie. Both must exist, " +
							"and the association is saved in the database."
		)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
		public async Task<IActionResult> PostLinkActorToMovie(
			[FromRoute] int movieId,
			[FromRoute] int actorId)
		{

			var actor = await _context.Actors
				.FirstOrDefaultAsync(a => a.Id == actorId);

			if (actor is null)
			{
				return Problem(
					statusCode: StatusCodes.Status404NotFound,
					title: "Invalid actor ID",
					detail: $"No actor with ID {actorId} was found.",
					instance: HttpContext.Request.Path
				);
			}

			var movie = await _context.Movies
				.FirstOrDefaultAsync(m => m.Id == movieId);

			if (movie is null)
			{
				return Problem(
					statusCode: StatusCodes.Status404NotFound,
					title: "Invalid movie ID",
					detail: $"No movie with ID {movie} was found.",
					instance: HttpContext.Request.Path
				);
			}

			movie.Actors.Add(actor);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		//// DELETE: api/Actors/5
		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteActor(int id)
		//{
		//	var actor = await _context.Actors.FindAsync(id);
		//	if (actor == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.Actors.Remove(actor);
		//	await _context.SaveChangesAsync();

		//	return NoContent();
		//}

		private bool ActorExists(int id)
		{
			return _context.Actors.Any(e => e.Id == id);
		}
	}
}
