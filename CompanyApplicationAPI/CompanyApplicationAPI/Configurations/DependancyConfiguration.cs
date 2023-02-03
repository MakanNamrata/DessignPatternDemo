using CompanyApplication.Core.Contract;
using CompanyApplication.Core.Service;
using CompanyApplication.Core.Service.Helper;
using CompanyApplication.Infra.Contract;
using CompanyApplication.Infra.Repository;

namespace CompanyApplicationAPI.Configurations
{
    public static class DependancyConfiguration
    {
        public static void AddDependancy(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<FileUploadHelper>();
        }
    }
}
