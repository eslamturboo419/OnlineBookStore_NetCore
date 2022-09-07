using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly OnlineBookCoreDBContext db;

        public UsersController(OnlineBookCoreDBContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserRole") != "Admin") { return RedirectToAction("Login"); }
            var val = db.Usersaccounts.ToList();
            return View(val);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usersaccount usersaccount)
        {
            if (ModelState.IsValid)
            {
                usersaccount.Role = "Customer";
                db.Usersaccounts.Add(usersaccount);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(usersaccount);
        }

        [HttpGet]
        public IActionResult EditAccount()
        {

           var id=   HttpContext.Session.GetString("UserId");
           if (id == null) { return NotFound(); }
           
            int id2= Convert.ToInt32(id);
           
           var val = db.Usersaccounts.Where(x => x.Id == id2).FirstOrDefault();
           if (val == null) { return NotFound(); }

            return View(val);
        }

        [HttpPost]
        public IActionResult EditAccount(Usersaccount usersaccount)
        {
            if (ModelState.IsValid)
            {
                db.Usersaccounts.Update(usersaccount);
                db.SaveChanges();

                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Users");
            }

            return View(usersaccount);
        }




        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserId") != null) { return RedirectToAction("Index","Home"); }

            return View();
        }


        [HttpPost]
        public IActionResult Login(Usersaccount usersaccount)
        {
            if (ModelState.IsValid)
            {
                var val = db.Usersaccounts.Where(x => x.Name == usersaccount.Name && x.Pass == usersaccount.Pass).FirstOrDefault();
                if (val == null) { return NotFound(); }

                HttpContext.Session.SetString("UserId", val.Id.ToString());
                HttpContext.Session.SetString("UserName", val.Name);
                HttpContext.Session.SetString("UserRole", val.Role);


                if(val.Role== "Customer")
                {
                    return RedirectToAction("Catalogue", "Books");
                }
                if (val.Role == "Admin")
                {
                    return RedirectToAction("Index", "Books");
                }


            }
            return View(usersaccount);
        }

 

        public IActionResult LogOut()
        {
            
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
