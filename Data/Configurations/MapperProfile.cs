using AutoMapper;
using MovieApi.Models.DTOs.MovieDtos;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MapperProfile: Profile
{
	public MapperProfile()
	{
		CreateMap<Movie, MovieWithGenreDto>()
			.ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.MoviesGenre!.Genre));
	}
}
