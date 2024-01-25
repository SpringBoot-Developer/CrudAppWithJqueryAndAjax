using HarrierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HarrierApp.Controllers
{
    public class UserPanelController : Controller
    {
        DbServices dbServices = new DbServices();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel um)
        {
            dbServices.AddUser(um);
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Login(string email)
        {
            var row=dbServices.GetUser().Where(model=> model.Email == email).FirstOrDefault();

            return View(row);
        }
        [HttpPost]
        public ActionResult Login(UserModel um)
        {
            var data = dbServices.GetUser().Where(model => model.Email == um.Email &&  model.Password == um.Password).FirstOrDefault(); 

            if (data != null)
            {
                Session["uid"] = um.Email;
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.ShowMsg = "Invalid Email or Password !!";
                ModelState.Clear();
            }

            return View();
        }

        public ActionResult Welcome(UserModel um)
        {
            var row = dbServices.GetUserByEmail(Session["uid"].ToString());
            return View(row);
        }



        public ActionResult Edit(int id)
        {
            var user = dbServices.GetUser().Where(model => model.Id == id).FirstOrDefault();
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {

            dbServices.UpdateUser(user);
            return RedirectToAction("Welcome");
        }




    }





}