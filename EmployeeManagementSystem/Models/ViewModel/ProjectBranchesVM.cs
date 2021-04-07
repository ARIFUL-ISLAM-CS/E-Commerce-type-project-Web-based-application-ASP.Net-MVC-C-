using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ViewModel
{
    public class ProjectBranchesVM
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public List<Branch> BranchList { get; set; }
        public List<ProjectVM> ProjectList { get; set; }
    }
}