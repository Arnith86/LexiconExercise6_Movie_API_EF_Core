using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

/// <summary>
/// Responsible for seeding the database with sample data using the Bogus library.
/// Populates tables such as Movies, Actors, MovieGenres, MovieDetails, and Reviews.
/// This is intended for development or testing and runs only if the database is empty.
/// </summary>
internal class DataSeeder
{
	private static Faker faker = new Faker("sv");
	private static List<string> _languages = new List<string> { "English", "Swedish", "French", "Spanish", "German" };

	private static MovieApiContext _context;

	internal static async Task InitAsync(MovieApiContext context)
	{
		_context = context;

		if (await context.Movies.AnyAsync()) return;

		int nrOfActiveActors = faker.Random.Int(0, 100);

		// Generate base entities
		IList<Actor> actors = GenerateActors(100);
		await context.Actors.AddRangeAsync(actors);

		IList<MovieGenre> movieGenre = GenerateMovieGenre();

		var movies = await GenerateMoviesAsync(50, movieGenre, actors);
		await context.SaveChangesAsync();


		// Generate and link entities with relations
		AssignActorsToMovie(actors, movies, nrOfActiveActors);
		var movieDetails = await GenerateMoviesDetailsAsync(movies);

		await context.SaveChangesAsync();
	}

	private static async Task<IEnumerable<MovieDetails>> GenerateMoviesDetailsAsync(IEnumerable<Movie> movies)
	{
		List<MovieDetails> movieDetails = new List<MovieDetails>();

		foreach (var movie in movies)
		{
			movie.MoviesDetails = new MovieDetails
			{
				Movie = movie,          // Establishes a foreignKey relationship
				Synopsis = faker.Lorem.Sentence(30),
				Language = faker.PickRandom(_languages),
				Budget = faker.Random.Int(300000, 5000000)
			};

			await _context.MovieDetails.AddAsync(movie.MoviesDetails);
			movieDetails.Add(movie.MoviesDetails);
		}

		return movieDetails;
	}

	private static IList<Actor> GenerateActors(int numberOfActors)
	{
		var actors = new List<Actor>(numberOfActors);

		for (int i = 0; i < numberOfActors; i++)
		{
			var fName = faker.Name.FindName();
			var fYear = faker.Random.Int(1945, 2020);


			actors.Add(new Actor
			{
				Name = fName,
				BirthYear = fYear
			});
		}

		return actors;
	}

	private static async Task<IEnumerable<Movie>> GenerateMoviesAsync(
		int numberOfMovies,
		IList<MovieGenre> movieGenres,
		IList<Actor> actors)
	{
		Random random = new Random();
		var movies = new List<Movie>(numberOfMovies);

		for (int i = 0; i < numberOfMovies; i++)
		{
			var fMovieTitle = "The " + faker.Company.CompanyName();
			var fYear = faker.Random.Int(1920, 2030);
			var fDuration = faker.Random.Int(5, 300);

			int nrOfReviews = random.Next(0, 4);
			int whichGenre = random.Next(0, movieGenres.Count - 1);
			//int whichActor = random.Next(0, nrOfActiveActors - 1);

			var movie = new Movie()
			{
				Title = fMovieTitle,
				Year = fYear,
				Duration = fDuration,
				Reviews = GenerateReviews(nrOfReviews),
				MoviesGenre = movieGenres[whichGenre]
			};

			//movie.Actors = AssignActorsToMovie(movie, actors[whichActor]);


			await _context.Movies.AddAsync(movie);
			movies.Add(movie);
		}

		return movies;
	}

	private static IList<MovieGenre> GenerateMovieGenre()
	{
		List<string> genreList = new List<string> { "Action", "Comedy", "Drama", "Sci-Fi", "Horror", "Romance" };
		List<MovieGenre> movieGenres = new List<MovieGenre>();


		foreach (string genre in genreList)
		{
			MovieGenre movieGenre = new MovieGenre()
			{
				Genre = genre
			};

			_context.MovieGenres.AddAsync(movieGenre);
			movieGenres.Add(movieGenre);
		}

		return movieGenres;
	}


	private static void AssignActorsToMovie(
		IEnumerable<Actor> actors,
		IEnumerable<Movie> movies,
		int activeActors)
	{
		//var actorsInMovies = new Collection<Actor>();
		var actorList = actors.Take(activeActors);

		movies.ToList().ForEach(movie =>
		{
			IEnumerable<Actor> selectedActors = faker.PickRandom(actorList, faker.Random.Int(1, 8));

			foreach (var actor in selectedActors)
			{
				movie.Actors.Add(actor);
				actor.Movies.Add(movie);
			}
		});




		//return actorsInMovies;
	}

	private static ICollection<Review> GenerateReviews(int nrOfReviews)
	{
		List<Review> reviews = new List<Review>();

		for (int i = 0; i < nrOfReviews; i++)
		{
			reviews.Add(new Review
			{
				ReviewerName = faker.Name.FindName(),
				Comment = faker.Lorem.Sentence(10),
				Rating = faker.Random.Int(1, 5)
			});
		}

		return reviews;
	}
}