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
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Request.Cookies["logindata"] != null)
            {
                Session["Admin"] = Request.Cookies["logindata"].Values["Admin"].ToString();
                return RedirectToAction("show", "user");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAdmin login , bool saveme) 
        {
            var Result = client.GetAsync("admin/getloginadmin/"+login.Phone +"/"+login.Password).Result;
            var admin = Result.Content.ReadAsAsync<Admin>().Result;

            if (admin != null)
            {

                if(saveme == true)
                {
                    HttpCookie co = new HttpCookie("logindata");
                    co.Values.Add("id", admin.Id.ToString());
                    co.Values.Add("name", admin.Name);
                    co.Values.Add("Admin", admin.ToString());

                    co.Expires= DateTime.Now.AddDays(1);

                    Response.Cookies.Add(co);
                }

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

            HttpCookie c = new HttpCookie("logindata");
            c.Expires = DateTime.Now.AddDays(-2);
            Response.Cookies.Add(c);

            return RedirectToAction("login");
        }
    }
}