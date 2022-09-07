using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private readonly OnlineBookCoreDBContext db;

        public OrderController( OnlineBookCoreDBContext db )
        {
            this.db = db;
        }
 

        public IActionResult Create( int ? id)
        {
            if (id == null) { return NotFound(); }

            var val = db.Books.Find(id);
            return View(val);

        }

        [HttpPost] 
        public async Task<IActionResult> Create(int bookId, int quantity)
        {
            Order order = new Order();
            order.BookId = bookId;
            order.Quantity = quantity;

            order.Userid = Convert.ToInt32(HttpContext.Session.GetString("userid")); ;
            order.Orderdate = DateTime.Today;
            db.Orders.Add(order);
            await db.SaveChangesAsync();
 


            return RedirectToAction("MyOrders");
        }


        public async Task<IActionResult> MyOrders()
        {

            int userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var orItems = await db.Orders.Where(x=>x.Userid == userid).ToListAsync();
            return View(orItems);

        }






    }
}
