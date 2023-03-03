using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class PostController : Controller
    {

        public HttpClient client = new HttpClient();

        public PostController()
        {
            client.BaseAddress = new Uri("https://trainapiegypt.azurewebsites.net/api/");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            try
            {
                var result = client.GetAsync("post/getallposts").Result;

                var posts = result.Content.ReadAsAsync<List<Post>>().Result;

                if (posts == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(posts);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public  ActionResult Delete(int id)
        {
            var result = client.DeleteAsync("post/deletepost/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("show");
            }
            else
            {
                return View("Error");
            }
        }
    }
}