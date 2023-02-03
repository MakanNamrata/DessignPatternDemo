using CompanyApplication.Infra.Domain.Entity;
using CompanyApplication.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Infra.Contract
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployee(Employee employee);

        Task<PagedList<Employee>> GetEmployees(string searchTerm = null, int page = 1, int pageSize = 5);

        Task<Employee?> GetEmployee(int EmployeeId);

        Task<int> UpdateEmployeeAsync(Employee employee, int employeeId);

        Task<string> GetFile(int employeeId);

    }
}
