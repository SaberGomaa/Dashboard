using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class OperationController : Controller
    {
        public HttpClient client = new HttpClient();
        public OperationController()
        {
            client.BaseAddress = new Uri("https://trainapiegypt.azurewebsites.net/api/");
        }
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAdmin login ) 
        {
            var Result = client.GetAsync("admin/getloginadmin/"+login.Phone +"/"+login.Password).Result;
            var admin = Result.Content.ReadAsAsync<Admin>().Result;

            if (admin != null)
            {
                Session.Add("Admin", admin);

                return RedirectToAction("show", "user");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;

            return RedirectToAction("login");
        }
    }
}