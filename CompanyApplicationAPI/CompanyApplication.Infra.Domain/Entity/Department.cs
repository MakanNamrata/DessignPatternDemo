using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Infra.Domain.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected Department()
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;    
        }
    }
}
