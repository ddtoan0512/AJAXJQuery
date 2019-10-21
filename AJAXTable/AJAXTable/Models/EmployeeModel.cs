using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJAXTable.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public bool Status { get; set; }
    }
}