using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisdale_Project_2
{
    class Employee : Person
    {
        private string department;

        public string Department
        {
            get => department;
            set
            {
                if (value.Length > 1)
                {
                    department = value;
                }
                else
                {
                    throw new ArgumentException("Department must be at least 2 characters long.");
                }

            }
        }


        public Employee(string firstName, string lastName, string address, DateTime dateOfBirth, string department) 
            : base(firstName, lastName, address, dateOfBirth)
        {
            Department = department;
        }
    }
}
