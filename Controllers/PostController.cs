﻿using System;
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
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
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

                var AcceptedCriricalPosts = posts.Where(c=>c.Critical == true).ToList();

                if (AcceptedCriricalPosts == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(AcceptedCriricalPosts);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult AbrovePosts()
        {
            try
            {
                var result = client.GetAsync("post/getallposts").Result;

                var posts = result.Content.ReadAsAsync<List<Post>>().Result;

                var AcceptedCriricalPosts = posts.Where(c => c.Critical == false).ToList();

                if (AcceptedCriricalPosts == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(AcceptedCriricalPosts);
                }
            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Abrove(int id)
        {

            var res = client.GetAsync("post/getpost/"+id).Result;

            var post = res.Content.ReadAsAsync<Post>().Result;

            post.Critical= true;

            var result = client.PutAsJsonAsync("Post/UpdatePost/" + post.Id, post).Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("AbrovePosts");
            else
                return View("Error");
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