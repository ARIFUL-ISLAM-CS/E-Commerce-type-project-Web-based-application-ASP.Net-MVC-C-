using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace EmployeeManagementSystem.Models
{
    public partial class EmployeeModel : DbContext
    {
        public EmployeeModel()
            : base("name=EmployeeModel")
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<EmployeeAjax> EmployeeAjaxes { get; set; }
        public virtual DbSet<Employeetblvm> Employeetblvms { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<tblRoles> TblRoles { get; set; }
        public virtual DbSet<tblUsers> TblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.EmployeeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.EmployeeDOB)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAjax>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Employeetblvm>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<Employeetblvm>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employeetblvm>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employeetblvm>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
               .Property(e => e.BranchName)
               .IsUnicode(false);

            modelBuilder.Entity<Project>()
               .Property(e => e.ProjectName)
               .IsUnicode(false);

            modelBuilder.Entity<Project>()
               .Property(e => e.TeamLeader)
               .IsUnicode(false);

            modelBuilder.Entity<Project>()
               .Property(e => e.ImagePath)
               .IsUnicode(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
