namespace EmployeeManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("EmployeeAjax")]
    public partial class EmployeeAjax
    {
        [Key]
        public int EmployeeID { get; set; }

        [StringLength(50)]
        public string EmployeeName { get; set; }

        [StringLength(50)]
        public string EmployeeAddress { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(50)]
        public string EmployeeDOB { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public EmployeeAjax()
        {
            ImagePath = "~/AppFiles/Images/default.png";
        }
    }
}
