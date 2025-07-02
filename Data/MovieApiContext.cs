using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
	public MovieApiContext(DbContextOptions<MovieApiContext> options)
		: base(options)
	{
	}

	public DbSet<MovieApi.Models.Entities.Movie> Movies { get; set; } = default!;
	public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
	public DbSet<Review> Reviews { get; set; } = default!;
	public DbSet<Actor> Actors { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		// Sets up a 1:1 relationship between Movies and MovieDetails. Can be null.
		modelBuilder.Entity<Movie>()
			.HasOne(m => m.MovieDetails)
			.WithOne(md => md.Movie)
			.HasForeignKey<MovieDetails>(md => md.MovieId)
			.IsRequired(false);


		// Sets up a 1:N relationship between Movies and Review. Can not be null.
		modelBuilder.Entity<Review>()
			.HasOne(m => m.Movie)
			.WithMany(r => r.Reviews)
			.HasForeignKey(r => r.MovieId);


		base.OnModelCreating(modelBuilder);
	}
}
