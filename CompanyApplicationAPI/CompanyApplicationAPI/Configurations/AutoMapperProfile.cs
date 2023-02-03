using AutoMapper;
using CompanyApplication.Core.Domain.ResponseModel;
using CompanyApplication.Infra.Domain.Entity;
using CompanyApplication.Shared;

namespace CompanyApplicationAPI.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PagedList<Employee>, PagedList<EmployeeResponseModel>>();
            CreateMap<Employee, EmployeeResponseModel>();
        }
    }
}
