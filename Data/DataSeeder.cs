using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

internal class DataSeeder
{
	private static Faker faker = new Faker("sv");
	private static List<string> _languages = new List<string> { "English", "Swedish", "French", "Spanish", "German" };
	private static List<string> genreList = new List<string> { "Action", "Comedy", "Drama", "Sci-Fi", "Horror", "Romance" };

	internal static async Task InitAsync(MovieApiContext context)
	{
		if (await context.Movies.AnyAsync()) return;

		var actors = GenerateActors(50);
		await context.AddRangeAsync(actors);
		await context.SaveChangesAsync();

		var movies = GenerateMovies(100);
		await context.AddRangeAsync(movies);
	}

	private static IEnumerable<Actor> GenerateActors(int numberOfActors)
	{
		var actors = new List<Actor>(numberOfActors);

		for (int i = 0; i < numberOfActors; i++)
		{
			var fName = faker.Name.FindName() + faker.Name.LastName();
			var fYear = faker.Random.Int(1945, 2020);


			actors.Add(new Actor
			{
				Name = fName,
				BirthYear = fYear
			});
		}

		return actors;
	}

	private static IEnumerable<Movie> GenerateMovies(int numberOfMovies)
	{
		var movies = new List<Movie>(numberOfMovies);

		//for (int i = 0; i < numberOfMovies; i++)
		//{
		//	var fMovieTitle = "The " + faker.Company.CompanyName();
		//	var fYear = faker.Random.Int(1920, 2030);
		//	var fDuration = faker.Random.Int(5, 300);

		//	var Movie = new Movie()
		//	{
		//		Title = fMovieTitle,
		//		Year = fYear,
		//		Duration = fDuration,
		//		MoviesDetails = new MovieDetails
		//		{
		//			Synopsis = faker.Lorem.Sentence(50),
		//			Language = faker.PickRandom(_languages),
		//			Budget = faker.Random.Int(300000, 5000000)
		//		},
		//		Review = new Review
		//		{

		//		}

		//	};

		//}

		//throw new NotImplementedException();
		return movies;
	}
}