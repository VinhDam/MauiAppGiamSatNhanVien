using API.Models;
using API.Models.DTO.LocationDTO;
using API.Models.DTO.ShiftDTO;
using AutoMapper;

namespace API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, UpdateLocationDTO>().ReverseMap();

            CreateMap<Shift, ShiftDTO>().ReverseMap();
            CreateMap<Shift, UpdateShiftDTO>().ReverseMap();
        }
    }
}
