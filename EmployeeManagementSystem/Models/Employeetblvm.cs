namespace EmployeeManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employeetblvm")]
    public partial class Employeetblvm
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        public DateTime EmployeeDOB { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }
    }
}
