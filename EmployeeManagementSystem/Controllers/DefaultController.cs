using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            var ctx = new EmployeeModel();

            var BranchWiseProjectQty = from p in ctx.Projects
                                         group p by p.BranchId into g
                                         select new
                                         {
                                             g.FirstOrDefault().BranchId,
                                             Qty = g.Sum(s => s.Mamber)
                                         };
            var listBranch = (from c in ctx.Branches
                                join cwpq in BranchWiseProjectQty on c.BranchId equals cwpq.BranchId
                              select new BranchesVM
                                {
                                    BranchName = c.BranchName,
                                    BranchId = cwpq.BranchId,
                                    Mamber = cwpq.Qty
                                }).ToList();
            var listProject = (from p in ctx.Projects
                               join c in ctx.Branches on p.BranchId equals c.BranchId
                               where p.BranchId == id
                               select new ProjectVM
                               {
                                   BranchId = p.BranchId,
                                   BranchName = c.BranchName,
                                   StartDate = p.StartDate,
                                   ImagePath = p.ImagePath,
                                   Budget = p.Budget,
                                   ProjectId = p.ProjectId,
                                   ProjectName = p.ProjectName,
                                   TeamLeader = p.TeamLeader,
                                   Mamber = p.Mamber
                               }).ToList();

            var oBranchWiseProjectVM = new BranchWiseProjectVM();
            oBranchWiseProjectVM.BranchList = listBranch;
            oBranchWiseProjectVM.ProjectList = listProject;
            oBranchWiseProjectVM.BranchId = listProject.Count > 0 ? listProject[0].BranchId : 0;
            oBranchWiseProjectVM.BranchName = listProject.Count > 0 ? listProject[0].BranchName : "";

            return View(oBranchWiseProjectVM);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Create()
        {
            var model = new ProjectBranchesVM();
            var ctx = new EmployeeModel();
            model.BranchList = ctx.Branches.ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Create(Branch model, string[] ProjectName,string[] TeamLeader, decimal[] Budget, int[] Mamber, DateTime[] StartDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new EmployeeModel();
            var oBranches = (from c in ctx.Branches where c.BranchName == model.BranchName.Trim() select c).FirstOrDefault();
            if (oBranches == null)
            {
                ctx.Branches.Add(model);
                ctx.SaveChanges();
            }
            else
            {
                model.BranchId = oBranches.BranchId;
            }

            var listProject = new List<Project>();
            for (int i = 0; i < ProjectName.Length; i++)
            {
                string imgPath = "";
                if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imgFile[i].FileName);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    imgFile[i].SaveAs(fileLocation);

                    imgPath = "/uploads/" + imgFile[i].FileName;
                }

                var newProject = new Project();
                newProject.ProjectName = ProjectName[i];
                newProject.TeamLeader = TeamLeader[i];
                newProject.Mamber = Mamber[i];
                newProject.Budget = Budget[i];
                newProject.StartDate = StartDate[i];
                newProject.ImagePath = imgPath;
                newProject.Mamber = Mamber[i];
                newProject.BranchId = model.BranchId;
                listProject.Add(newProject);
            }
            ctx.Projects.AddRange(listProject);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(int id)
        {
            var ctx = new EmployeeModel();
            var oProject = (from p in ctx.Projects
                            join c in ctx.Branches on p.BranchId equals c.BranchId
                            where p.ProjectId == id
                            select new ProjectVM
                            {
                                BranchId = p.BranchId,
                                BranchName = c.BranchName,
                                StartDate = p.StartDate,
                                ImagePath = p.ImagePath,
                                Budget = p.Budget,
                                ProjectId = p.ProjectId,
                                ProjectName = p.ProjectName,
                                TeamLeader = p.TeamLeader,
                                Mamber = p.Mamber
                            }).FirstOrDefault();
            oProject.BranchList = ctx.Branches.ToList(); // for showing category list in view
            return View(oProject);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(ProjectVM model)
        {
            var ctx = new EmployeeModel();

            string imgPath = "";
            if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImgFile.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                model.ImgFile.SaveAs(fileLocation);

                imgPath = "/uploads/" + model.ImgFile.FileName;
            }

            var oProject = ctx.Projects.Where(w => w.ProjectId == model.ProjectId).FirstOrDefault();
            if (oProject != null)
            {
                oProject.ProjectName = model.ProjectName;
                oProject.TeamLeader = model.TeamLeader;
                oProject.Mamber = model.Mamber;
                oProject.Budget = model.Budget;
                oProject.StartDate = model.StartDate;
                oProject.BranchId = model.BranchId;
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var fileName = Path.GetFileName(oProject.ImagePath);
                    string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                oProject.ImagePath = imgPath == "" ? oProject.ImagePath : imgPath;

                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult EditMultiple(int id)
        {
            var ctx = new EmployeeModel();
            var oBranchWiseProjectVM = new BranchWiseProjectVM();
            var listProject = (from p in ctx.Projects
                               join c in ctx.Branches on p.BranchId equals c.BranchId
                               where p.BranchId == id
                               select new ProjectVM
                               {
                                   BranchId = p.BranchId,
                                   BranchName = c.BranchName,
                                   StartDate = p.StartDate,
                                   ImagePath = p.ImagePath,
                                   Budget = p.Budget,
                                   ProjectId = p.ProjectId,
                                   ProjectName = p.ProjectName,
                                   TeamLeader = p.TeamLeader,
                                   Mamber = p.Mamber
                               }).ToList();
            oBranchWiseProjectVM.ProjectList = listProject;
            // for showing category list in view
            oBranchWiseProjectVM.BranchList = (from c in ctx.Branches
                                                 select new BranchesVM
                                                 {
                                                     BranchId = c.BranchId,
                                                     BranchName = c.BranchName
                                                 }).ToList();
            oBranchWiseProjectVM.BranchId = listProject.Count > 0 ? listProject[0].BranchId : 0;
            oBranchWiseProjectVM.BranchName = listProject.Count > 0 ? listProject[0].BranchName : "";
            return View(oBranchWiseProjectVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult EditMultiple(Branch model, int[] ProjectId, string[] ProjectName, string[] TeamLeader, decimal[] Budget, int[] Mamber, DateTime[] StartDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new EmployeeModel();
            var ListProject = new List<Project>();
            for (int i = 0; i < ProjectName.Length; i++)
            {
                if (ProjectId[i] > 0)
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }
                    int pid = ProjectId[i];
                    var oProject = ctx.Projects.Where(w => w.ProjectId == pid).FirstOrDefault();
                    if (oProject != null)
                    {
                        oProject.ProjectName = ProjectName[i];
                        oProject.TeamLeader = TeamLeader[i];
                        oProject.Mamber = Mamber[i];
                        oProject.Budget = Budget[i];
                        oProject.StartDate = StartDate[i];
                        oProject.BranchId = model.BranchId;
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            var fileName = Path.GetFileName(oProject.ImagePath);
                            string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                        }
                        oProject.ImagePath = imgPath == "" ? oProject.ImagePath : imgPath;
                        ctx.SaveChanges();
                    }
                }
                else if (!string.IsNullOrEmpty(ProjectName[i]))
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }

                    var newProject = new Project();
                    newProject.ProjectName = ProjectName[i];
                    newProject.TeamLeader = TeamLeader[i];
                    newProject.Mamber = Mamber[i];
                    newProject.Budget = Budget[i];
                    newProject.StartDate = StartDate[i];
                    newProject.ImagePath = imgPath;
                    newProject.BranchId = model.BranchId;
                    ctx.Projects.Add(newProject);
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int id)
        {
            var ctx = new EmployeeModel();
            var oProject = ctx.Projects.Where(p => p.ProjectId == id).FirstOrDefault();
            if (oProject != null)
            {
                ctx.Projects.Remove(oProject);
                ctx.SaveChanges();

                var fileName = Path.GetFileName(oProject.ImagePath);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(fileLocation))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(fileLocation);
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult DeleteMultiple(int id)
        {
            var ctx = new EmployeeModel();
            var listProject = ctx.Projects.Where(p => p.BranchId == id).ToList();
            foreach (var oProject in listProject)
            {
                if (oProject != null)
                {
                    ctx.Projects.Remove(oProject);
                    ctx.SaveChanges();

                    var fileName = Path.GetFileName(oProject.ImagePath);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(fileLocation))
                    {
                        // If file found, delete it    
                        System.IO.File.Delete(fileLocation);
                    }
                }
            }

            var oBranches = ctx.Branches.Where(c => c.BranchId == id).FirstOrDefault();
            ctx.Branches.Remove(oBranches);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}