using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

internal class DataSeeder
{
	private static Faker faker = new Faker("sv");

	internal static async Task InitAsync(MovieApiContext context)
	{
		if (await context.Movies.AnyAsync()) return;

		var actors = GenerateActors(50);
		await context.AddRangeAsync(actors);
		await context.SaveChangesAsync();

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

}