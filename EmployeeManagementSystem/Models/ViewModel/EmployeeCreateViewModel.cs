using EmployeeManagementSystem.Models.ViewModel;
using EmployeeManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models.ViewModels
{
    public class EmployeeCreateViewModel
    {
        public int EmployeeId { get; set; }
      
        [Required(ErrorMessage = "Employee Name Is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Employee Name")]
        [StringLength(50, ErrorMessage = "Player Name Must be within 50 Charecter")]
        [CustomExcludeCharacter("/.,!@#$%")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "Address Must be within 500 Charecter")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Wrong Phone Number")]
        public string Contact { get; set; }
        [DisplayName("Date Of Birth")]
        [CustomDateOfBirth(ErrorMessage = "Date of Birth must be less than or equal to Today's Date")]
        public System.DateTime EmployeeDOB { get; set; }
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}