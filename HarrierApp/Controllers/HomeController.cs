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
        public ActionResult Add(UserDetailModel user)
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
        public ActionResult Edit(UserDetailModel user)
        {

            dbServices.UpdateUserDetail (user);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Console.WriteLine("Delete");
            UserDetailModel user = dbServices.GetAllUserDetail().Find(model => model.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserDetailModel user)
        {
            Console.WriteLine("Delete..");
            dbServices.DeleteUserDetail(user);
            return RedirectToAction("Index");
        }
    }

}