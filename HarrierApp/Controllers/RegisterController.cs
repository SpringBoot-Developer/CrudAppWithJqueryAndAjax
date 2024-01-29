using HarrierApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace HarrierApp.Controllers
{
    public class RegisterController : Controller
    {


        DbServices dbServices = new DbServices();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(UserAuth user)
        {
            user.IsValid = false;
            dbServices.AddUserAuth(user);
            Console.WriteLine("Login User Saved into DB=" + user.Id);
            // BuildEmailTemplate(user.Id);
            return Json("Registration Successfull" , JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckValidUser(UserAuth user)
        {
            string result = "Fail";
            var LoginUser = dbServices.GetAllUserAuth().Where(x => x.Email == user.Email && x.Password == user.Password).SingleOrDefault();

            if(LoginUser != null)
            {
                Session["UserID"] = LoginUser.Id.ToString();
                Session["UserName"] = LoginUser.UserName.ToString();
                result = "Success";

            }
            return Json(result , JsonRequestBehavior.AllowGet);
        }


        public ActionResult AfterLogin()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");


            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");

        }


        /*  public ActionResult Confirm(int regId)
          {
              ViewBag.regId = regId;
              return View();  
          }*/
        /*
                public JsonResult RegisterConfirm(int regId)
                {
                    UserAuth Data = dbServices.GetAllUserAuth().Where(x => x.Id == regId).FirstOrDefault();
                    Data.IsValid = true;
                    dbServices.AddUserAuth(Data);
                    var msg = "Your Emaill Is Verified!";
                    return Json(msg, JsonRequestBehavior.AllowGet);


                }

                public void BuildEmailTemplate(int regID)   
                {
                    string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
                   // var regInfo = dbServices.GetUserById(regID);
                    var regInfo= dbServices.GetAllUserAuth().Where(x => x.Id == regID).FirstOrDefault();

                    var url = "https://localhost:44314/" + "Resgister/Confirm?regId=" + regID;
                    body = body.Replace("@ViewBag.ConfirmationLink" , url);
                    body = body.ToString();
                    BuildEmailTemplate("Your Account Is Successfully Created" , body , regInfo.Email);

                }

                private void BuildEmailTemplate(string subjectText , string bodyText , string sendTo)
                {
                    string from, to, bcc, cc, subject, body;
                    from = "rohan10bhakte0708@gmail.com";
                    to = sendTo.Trim();
                    bcc = "";
                    cc = "";
                    subject = subjectText;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(bodyText);
                    body = sb.ToString();
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(from);
                    mail.To.Add(new MailAddress(to));
                    if(!string.IsNullOrEmpty(bcc))
                        mail.Bcc.Add(new MailAddress(bcc));
                    if
                    (!string.IsNullOrEmpty(cc))

                        mail.CC.Add(new MailAddress(cc));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SendEmail(mail);
                }*/

        /*public static void SendEmail(MailMessage mail)
        {

            SmtpClient client = new SmtpClient();
            client.Host = "smtp-gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("rohan10bhakte0708@gmail.com" , "Rohan@8774");
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }*/
    }
}