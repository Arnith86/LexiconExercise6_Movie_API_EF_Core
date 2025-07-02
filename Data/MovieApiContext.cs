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

		// Sets up a 1:1 relationship between Movies and MoviesDetails. Can be null.
		modelBuilder.Entity<Movie>()
			.HasOne(m => m.MoviesDetails)
			.WithOne(md => md.Movie)
			.HasForeignKey<MovieDetails>(md => md.MovieId)
			.IsRequired(false);


		// Sets up a 1:N relationship between Movies and Review. Can not be null.
		modelBuilder.Entity<Review>()
			.HasOne(m => m.Movie)
			.WithMany(r => r.Reviews)
			.HasForeignKey(r => r.MovieId);

		// Defines the composite primary key of MovieGenre
		modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.Genre });


		// Sets up a 1:N relationship between Movies and MoviesGenre. Can be null.
		modelBuilder.Entity<MovieGenre>()
			.HasOne(m => m.Movie)
			.WithMany(mg => mg.movieGenres)
			.HasForeignKey(mg => mg.MovieId);

		base.OnModelCreating(modelBuilder);
	}
}
