using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
    public MovieApiContext (DbContextOptions<MovieApiContext> options)
        : base(options)
    {
    }

    public DbSet<MovieApi.Models.Entities.Movie> Movie { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

        // Sets up a 1:1 relationship between Movie and MovieDetails. Can be null.
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.MovieDetails)
            .WithOne(md => md.Movie)
            .HasForeignKey<MovieDetails>(md => md.MovieId)
            .IsRequired(false);



		base.OnModelCreating(modelBuilder);
	}
}
