using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }
        [AllowAnonymous]
        IEnumerable<EmployeeAjax> GetAllEmployee()
        {
            using (EmployeeModel db = new EmployeeModel())
            {
                return db.EmployeeAjaxes.ToList<EmployeeAjax>();
            }

        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AddOrEdit(int id = 0)
        {
            EmployeeAjax emp = new EmployeeAjax();
            if (id != 0)
            {
                using (EmployeeModel db = new EmployeeModel())
                {
                    emp = db.EmployeeAjaxes.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeAjax>();
                }
            }
            return View(emp);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AddOrEdit(EmployeeAjax emp)
        {
            try
            {
                if (emp.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.ImagePath = "~/AppFiles/Images/" + fileName;
                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (EmployeeModel db = new EmployeeModel())
                {
                    if (emp.EmployeeID == 0)
                    {
                        db.EmployeeAjaxes.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index", GetAllEmployee()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int id)
        {
            try
            {
                using (EmployeeModel db = new EmployeeModel())
                {
                    EmployeeAjax emp = db.EmployeeAjaxes.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeAjax>();
                    db.EmployeeAjaxes.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index", GetAllEmployee()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}