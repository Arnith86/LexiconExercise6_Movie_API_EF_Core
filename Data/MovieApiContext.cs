using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Configurations;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
	public MovieApiContext(DbContextOptions<MovieApiContext> options)
		: base(options)
	{
	}

	public DbSet<Movie> Movies { get; set; } = default!;
	public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
	public DbSet<MovieGenre> MovieGenres { get; set; } = default!;
	public DbSet<Review> Reviews { get; set; } = default!;
	public DbSet<Actor> Actors { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new MovieDetailsConfigurations());
		modelBuilder.ApplyConfiguration(new ReviewConfigurations());
		modelBuilder.ApplyConfiguration(new MovieGenreConfigurations());
		modelBuilder.ApplyConfiguration(new ActorConfigurations());
		modelBuilder.ApplyConfiguration(new MovieConfigurations());

		//// Defines the composite primary key of MovieGenre -- not used right now 
		//modelBuilder.Entity<MovieGenre>()
		//	.HasKey(mg => new { mg.MovieId, mg.Genre });


		//// Sets up a 1:N relationship between Movies and MoviesGenre. Can be null.
		//modelBuilder.Entity<MovieGenre>()
		//	.HasOne(mg => mg.Movie)
		//	.WithMany(m => m.MovieGenres)
		//	.HasForeignKey(mg => mg.MovieId)
		//	.OnDelete(DeleteBehavior.Cascade);


	}
}
