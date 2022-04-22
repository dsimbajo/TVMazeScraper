
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVMaze.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TVMaze.Core.DTO.Show, TVMaze.Core.Entities.Show>()
                .ForMember(dest => dest.Genre, act => act.MapFrom(src => src.Genres.FirstOrDefault()))
                .ForMember(dest => dest.Network, act => act.MapFrom(src => src.Network.Name));

            CreateMap<TVMaze.Core.DTO.Cast, TVMaze.Core.Entities.Cast>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.Url, act => act.MapFrom(src => src.Person.Url))
                .ForMember(dest => dest.Birthday, act => act.MapFrom(src => src.Person.Birthday))
                .ForMember(dest => dest.Deathday, act => act.MapFrom(src => src.Person.Deathday))
                .ForMember(dest => dest.Gender, act => act.MapFrom(src => src.Person.Gender))
                .ForMember(dest => dest.Character, act => act.MapFrom(src => src.Character.Name))
                .ForMember(dest => dest.CharacterUrl, act => act.MapFrom(src => src.Character.Url));
        }
    }
}
