using HarrierApp.Models;
using log4net;
using Microsoft.Identity.Client;
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
        public ActionResult Add(UsersModel user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    user.Skills = Request.Form.GetValues(name: "Skills")?.Where(skill => skill != "false").ToList() ?? new List<string>();
                    dbServices.Add(user);
                    Console.WriteLine(user);
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                // If ModelState is not valid, return the view with validation errors
                return View(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();

            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var user = dbServices.GetAll().Find(model => model.Id == id);
                if(user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(UsersModel user)
        {
            try
            {
                user.Skills = Request.Form.GetValues(name: "Skills")?.Where(skill => skill != "false").ToList() ?? new List<string>();
                dbServices.Update(user);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                UsersModel user = dbServices.GetAll().Find(model => model.Id == id);
                if(user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(UsersModel user)
        {
            try
            {
                dbServices.Delete(user);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }

    }

}