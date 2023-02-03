using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Infra.Domain.Entity
{
    public class Employee : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string CvFile { get; set; }


        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public Employee()
        {

        }

        public Employee(string name, string designation,string cvFile, int departmentId)
        {
            Name = name;
            Designation = designation;
            CvFile = cvFile;
            DepartmentId = departmentId;
            CreatedOn = DateTime.UtcNow;
            IsDeleted = false;
        }
        
        public Employee Delete()
        {
            IsDeleted = true;
            UpdatedOn = DateTime.Now;
            return this;
        }

    }
}
