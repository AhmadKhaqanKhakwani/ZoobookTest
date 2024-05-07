using AutoMapper;
using Model.Entities;
using Model.ViewModels;

namespace Model.AutoMappers
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();
        }
    }
}
