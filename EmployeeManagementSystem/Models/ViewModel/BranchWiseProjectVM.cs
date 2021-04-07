
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ViewModel
{
    public class BranchWiseProjectVM
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; }
        public List<BranchesVM> BranchList { get; set; }
        public List<ProjectVM> ProjectList { get; set; }
    }
}