using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
        public HttpClient client = new HttpClient();
        public AdminController()
        {
            client.BaseAddress = new Uri("https://egypttrainapi.azurewebsites.net/api/");
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult view()
        {
            try {
                var result = client.GetAsync("admin/getadmins").Result;

                var admins = result.Content.ReadAsAsync<List<Admin>>().Result;

                if (admins == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(admins);
                }
            }
            catch
            {
                return View("Error");
            }
            }
        public ActionResult Register()
        {
            return View();
        }
    }
    
}
