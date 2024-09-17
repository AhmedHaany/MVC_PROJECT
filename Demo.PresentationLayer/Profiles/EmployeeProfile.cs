using AutoMapper;
using Demo.DataAccessLayer.Models;
using Demo.PresentationLayer.ViewModels;

namespace Demo.PresentationLayer.Profiles
{
    public class EmployeeProfile : Profile 
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
