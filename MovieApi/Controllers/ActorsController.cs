using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.Entities;
using MovieData.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
	[Route("api/movie/{movieId}/actors")]
	[ApiController]
	public class ActorsController : ControllerBase
	{
		private readonly MovieApiContext _context;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ActorsController(MovieApiContext context, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_context = context;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}


		// POST /api/movies/5/actors
		/// <summary>
		/// Associates an existing actor with an existing movie, specifying their role.
		/// </summary>
		/// <param name="movieId">The ID of the movie to which the actor should be added.</param>
		/// <param name="movieActorCreateDto">The actor ID and their role in the movie.</param>
		/// <returns>No content on success; BadRequest if movie or actor ID is invalid.</returns>
		/// <response code="204">The actor was successfully associated with the movie.</response>
		/// <response code="400">Invalid movie or actor ID was provided.</response>
		[SwaggerOperation(
			Summary = "Add an actor to a movie.",
			Description = "Associates an existing actor with an existing movie by specifying their role. " +
						  "Requires a valid movie ID and actor ID."
		)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
		[HttpPost]
		public async Task<IActionResult> PostLinkMovieAndActor(
			[FromBody] MovieActorCreateDto movieActorCreateDto,
			[FromRoute] int movieId)
		{

			var movie = await _unitOfWork.Movies.GetMovieAsync(movieId, changeTracker: true);

			if (movie is null)
			{
				return Problem(
					statusCode: StatusCodes.Status400BadRequest,
					title: "Invalid movie ID",
					detail: $"No movie with ID {movieId} was found.",
					instance: HttpContext.Request.Path
				);
			}

			bool actorExists = await _unitOfWork.Actors.AnyAsync(movieActorCreateDto.ActorId);

			if (!actorExists)
			{
				return Problem(
					statusCode: StatusCodes.Status400BadRequest,
					title: "Invalid actor ID",
					detail: $"No actor with ID {movieId} was found.",
					instance: HttpContext.Request.Path
				);
			}

			MovieActor movieActor = _mapper.Map<MovieActor>(movieActorCreateDto);
			
			movie.MovieActors.Add(_mapper.Map<MovieActor>(movieActorCreateDto));


			await _unitOfWork.CompleteAsync();

			return NoContent();
		}
	}
}
