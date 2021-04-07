namespace EmployeeManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string TeamLeader { get; set; }

        public decimal Budget { get; set; }

        public string ImagePath { get; set; }

        public DateTime? StartDate { get; set; }

        public int Mamber { get; set; }

        public int BranchId { get; set; }
    }
}
