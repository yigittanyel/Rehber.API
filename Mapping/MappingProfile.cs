using AutoMapper;
using Rehber.API.Dto;
using Rehber.API.Models.Entities;

namespace Rehber.API.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<KisiDto, Kisi>();
        }
    }
}
