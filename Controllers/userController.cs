﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class userController : Controller
    {
        public HttpClient client = new HttpClient();
        public userController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public new ActionResult Profile(int id)
        {
            var result = client.GetAsync("user/getuserbyid?Id="+id).Result;

            var user = result.Content.ReadAsAsync<User>().Result;



            return View(user);
        }

        public ActionResult Show()
        {
            var result = client.GetAsync("user/getusers").Result;

            var users = result.Content.ReadAsAsync<List<User>>().Result;


            return View(users);
        }

        public ActionResult Delete(int id)
        {
            var result = client.DeleteAsync("user/DeleteUser?Id=" + id).Result;

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