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
        public ActionResult Add(User user)
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
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty , "An error occurred while processing your request.");
                return View(user);
            }
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