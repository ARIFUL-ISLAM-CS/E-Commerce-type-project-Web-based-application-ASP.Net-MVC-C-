using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.ViewModels
{
    internal class CustomDateOfBirthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}