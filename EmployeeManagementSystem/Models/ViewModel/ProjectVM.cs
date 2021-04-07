
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ViewModel
{
    public class ProjectVM
    {   
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string TeamLeader { get; set; }

        public decimal Budget { get; set; }

        public string ImagePath { get; set; }

        public DateTime? StartDate { get; set; }

        public int Mamber { get; set; }

        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public List<Branch> BranchList { get; set; }
    }
}