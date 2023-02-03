using CompanyApplication.Infra.Contract;
using CompanyApplication.Infra.Domain;
using CompanyApplication.Infra.Domain.Entity;
using CompanyApplication.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Infra.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDBContext _context;

        public EmployeeRepository(CompanyDBContext context)
        {
            _context = context;
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetEmployee(int EmployeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == EmployeeId);
            return employee;

        }

        public async Task<PagedList<Employee>> GetEmployees(string searchTerm = null, int page = 1, int pageSize = 5)
        {
            try
            {
                var employees = _context.Employees.Include(data => data.Department).Where(data => !data.IsDeleted).OrderByDescending(data=>data.CreatedOn).AsQueryable();

                if(!string.IsNullOrEmpty(searchTerm))
                {
                    employees = employees.Where(x =>
                        EF.Functions.Like(x.Name, $"%{searchTerm}%") ||
                        EF.Functions.Like(x.Designation, $"%{searchTerm}%")
                    );
                }

                var count = await employees.LongCountAsync();
                var pagedList = employees.ToPagedList(page, pageSize, count);

                return pagedList;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

       

        public async Task<string> GetFile(int employeeId)
        {
            var file = await _context.Employees.Where(x=>x.Id == employeeId).Select(x=>x.CvFile).FirstOrDefaultAsync();
            return file;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee, int employeeId)
        {
            var employees = await _context.Employees.Where(x => x.Id == employeeId && !x.IsDeleted).FirstAsync();
            employees.Designation = employee.Designation;
            employees.Name = employee.Name;
            employees.DepartmentId = employee.DepartmentId;
            employees.CvFile = employee.CvFile;
            _context.Employees.Update(employees);
            return await _context.SaveChangesAsync();
        }
    }
}
