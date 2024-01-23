using HarrierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrierApp.Controllers
{
   
    public class UserController : Controller
    {

        DbServices dbServices = new DbServices();

        public ActionResult Index()
        {
            return View(dbServices.GetAll());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(User user)
        {
            if(ModelState.IsValid)
            {
                // Retrieve selected skills from the form data
                user.Skills = Request.Form.GetValues(name: "Skills")?.Where(skill => skill != "false").ToList() ?? new List<string>();

                // Add the user to the database
                dbServices.Add(user);

                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var user = dbServices.GetAll().Find(model => model.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            user.Skills = Request.Form.GetValues(name: "Skills")?.Where(skill => skill != "false").ToList() ?? new List<string>();

            dbServices.Update(user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            User user = dbServices.GetAll().Find(model => model.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(User user)
        {
            dbServices.Delete(user);
            return RedirectToAction("Index");
        }
    }

}