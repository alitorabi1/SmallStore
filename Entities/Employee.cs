using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    public class Employee
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int Phone { set; get; }
        public int CellPhone { set; get; }
        public string Address { set; get; }
        public string PostalCode { set; get; }
        public DateTime BirthDate { set; get; }
        public string JobTitle { set; get; }
        public DateTime HireDate { set; get; }
        public DateTime FireDate { set; get; }
        public decimal Salary { set; get; }
        public int IsPermenant { set; get; }
        public int IsManager { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public byte[] EmployeeImage { set; get; }
        public string SIN { set; get; }

    }
}
