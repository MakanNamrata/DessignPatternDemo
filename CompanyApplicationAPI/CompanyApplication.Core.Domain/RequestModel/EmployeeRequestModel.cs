using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Core.Domain.RequestModel
{
    public record EmployeeRequestModel
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public IFormFile CvFile { get; set; }
        public int DepartmentId { get; set; }
    }
}
