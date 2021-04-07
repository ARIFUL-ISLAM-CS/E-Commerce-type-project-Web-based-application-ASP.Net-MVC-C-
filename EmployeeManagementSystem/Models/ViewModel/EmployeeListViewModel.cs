using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ViewModels
{
    public class EmployeeListViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public System.DateTime EmployeeDOB { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }

    }
}