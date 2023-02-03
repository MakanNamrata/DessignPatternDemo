using CompanyApplication.Core.Domain.RequestModel;
using CompanyApplication.Infra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Core.Builder
{
    public class EmployeeBuilder
    {
        public static Employee Build(EmployeeRequestModel model, string cvKey, string createdByUserId = "")
        {
            return new Employee(model.Name, model.Designation, cvKey, model.DepartmentId);
        }
    }
}
