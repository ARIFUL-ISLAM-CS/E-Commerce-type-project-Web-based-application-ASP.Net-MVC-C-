using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class ViewModelController : Controller
    {
        // GET: ViewModel
        private EmployeeModel db = new EmployeeModel();
       [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var emp = from s in db.Employeetblvms
                          select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    emp = emp.Where(s => s.EmployeeName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        emp = emp.OrderByDescending(s => s.EmployeeName);
                        break;

                    default:  // Name ascending
                        emp = emp.OrderBy(s => s.EmployeeName);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(emp.ToPagedList(pageNumber, pageSize));


            //    List<EmployeeListViewModel> list = db.Employeetblvms.Select(t => new EmployeeListViewModel
            //{
            //    EmployeeId = t.EmployeeId,
            //    EmployeeName = t.EmployeeName,
            //    Address = t.Address,
            //    Email = t.Email,
            //    Contact = t.Contact,
            //    EmployeeDOB = t.EmployeeDOB,
            //    ImageName = t.ImageName,
            //    ImageUrl = t.ImageUrl
            //}).ToList();
            //return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AddOrEdit(EmployeeCreateViewModel viewObj)
        {
            var result = false;
            string fileName = Path.GetFileNameWithoutExtension(viewObj.ImageFile.FileName);
            string extension = Path.GetExtension(viewObj.ImageFile.FileName);
            string fileWithExtension = fileName + extension;
            Employeetblvm trObj = new Employeetblvm();
            trObj.EmployeeName = viewObj.EmployeeName;
            trObj.Address = viewObj.Address;
            trObj.Email = viewObj.Email;
            trObj.Contact = viewObj.Contact;
            trObj.EmployeeDOB = viewObj.EmployeeDOB;
            trObj.ImageName = fileWithExtension;
            trObj.ImageUrl = "~/Images/" + fileName + extension;
            string serverPath = Path.Combine(Server.MapPath("~/Images/" + fileName + extension));
            viewObj.ImageFile.SaveAs(serverPath);
            if (ModelState.IsValid)
            {
                if (viewObj.EmployeeId == 0)
                {
                    db.Employeetblvms.Add(trObj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    trObj.EmployeeId = viewObj.EmployeeId;
                    db.Entry(trObj).State = EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (viewObj.EmployeeId == 0)
                {
                    return View("Create");
                }
                else
                {
                    return View("Edit");
                }
            }
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(int id)
        {
            Employeetblvm trObj = db.Employeetblvms.SingleOrDefault(t => t.EmployeeId == id);
            EmployeeCreateViewModel viewObj = new EmployeeCreateViewModel();
            viewObj.EmployeeId = trObj.EmployeeId;
            viewObj.EmployeeName = trObj.EmployeeName;
            viewObj.Address = trObj.Address;
            viewObj.Email = trObj.Email;
            viewObj.Contact = trObj.Contact;
            viewObj.EmployeeDOB = trObj.EmployeeDOB;
            viewObj.ImageUrl = trObj.ImageUrl;
            viewObj.ImageName = trObj.ImageName;
            return View(viewObj);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int id)
        {
            Employeetblvm trObj = db.Employeetblvms.SingleOrDefault(t => t.EmployeeId == id);
            {
                db.Employeetblvms.Remove(trObj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}