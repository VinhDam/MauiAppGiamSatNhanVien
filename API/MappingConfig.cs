using API.Models;
using API.Models.DTO;
using AutoMapper;

namespace API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();

            CreateMap<Shift, ShiftDTO>().ReverseMap();

            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
