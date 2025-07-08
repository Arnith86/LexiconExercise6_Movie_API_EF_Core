using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;
using System.Reflection.Emit;

namespace MovieApi.Data.Configurations
{
	internal class MovieDetailsConfigurations : IEntityTypeConfiguration<MovieDetails>
	{
		public void Configure(EntityTypeBuilder<MovieDetails> builder)
		{
			builder.HasKey(md => md.Id);

			builder.Property(md => md.Synopsis)
				.HasMaxLength(400);

			builder.Property(md => md.Language)
				.HasMaxLength(50);

			// Sets up a 1:1 relationship between Movies and MoviesDetails. 
			builder.HasOne(md => md.Movie)
				.WithOne(m => m.MoviesDetails)
				.HasForeignKey<MovieDetails>(m => m.MovieId)
				.IsRequired(true);

			builder.ToTable("MoviesDetails");
		}
	}
}