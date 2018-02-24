using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisdale_Project_2
{
    class Customer : Person
    {
        private string favoriteDepartment;

        public string FavoriteDepartment
        {
            get => favoriteDepartment;
            set
            {
                if (value.Length > 1)
                {
                    favoriteDepartment = value;
                }
                else
                {
                    throw new ArgumentException("Favorite department must be at least 2 characters long.");
                }

            }
        }


        public Customer(string firstName, string lastName, string address, DateTime dateOfBirth, string favoriteDepartment) 
            : base(firstName, lastName, address, dateOfBirth)
        {
            FavoriteDepartment = favoriteDepartment;
        }
    }
}
