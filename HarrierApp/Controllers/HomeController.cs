using HarrierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrierApp.Controllers
{

    public class HomeController : Controller
    {

        DbServices dbServices = new DbServices();

        public ActionResult Index()
        {
            return View(dbServices.GetAllUserDetail());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserDetail user)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("Saving Data");
                // Add the user to the database
                dbServices.AddUserDetail(user);

                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var user = dbServices.GetAllUserDetail().Find(model => model.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserDetail user)
        {

            dbServices.UpdateUserDetail (user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Console.WriteLine("Delete");
            UserDetail user = dbServices.GetAllUserDetail().Find(model => model.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserDetail user)
        {
            Console.WriteLine("Delete..");
            dbServices.DeleteUserDetail(user);
            return RedirectToAction("Index");
        }
    }

}