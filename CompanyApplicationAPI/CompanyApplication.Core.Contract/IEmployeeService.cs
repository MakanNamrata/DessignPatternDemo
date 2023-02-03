using CompanyApplication.Core.Domain.RequestModel;
using CompanyApplication.Core.Domain.ResponseModel;
using CompanyApplication.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Core.Contract
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(EmployeeRequestModel employee);

        Task UpdateEmployee(EmployeeRequestModel employee, int employeeId);

        Task<PagedList<EmployeeResponseModel>> GetEmployeeAsync(string searchTerm = null, int page=1,int pageSize = 5); 

        Task DeleteEmployeeAsync(int employeeId);

        Task<string> ViewFile(int employeeId);

        Task<string> DownloadFile(int employeeId);

        Task<string> DownloadFromCloudinary(int employeeId);

    }
}
