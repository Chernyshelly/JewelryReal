using JewelryReal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Products()
        {
            var products = db.Products.Include(pt => pt.Product_type).Include(m => m.Materials);
            return View(await products.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Product_types = new SelectList(db.Product_types, "TypeID", "Type_name");
            return View();
        }
        public IActionResult DeleteFail()
        {
            return View();
        }
        public IActionResult EditFail()
        {
            return View();
        }
        public IActionResult CreateFail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            try
            {
                Console.WriteLine($"{client.Number_of_regular_customers_card} n={client.Name} d={client.Discount.Discount_percent} {client.Discount.Discount_name}");
                client.Discount = await db.Discounts.FirstOrDefaultAsync(dd => dd.Discount_percent == client.Discount.Discount_percent);
                Console.WriteLine($"2{client.Number_of_regular_customers_card} n={client.Name} d={client.Discount.Discount_percent} {client.Discount.Discount_name}");
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Clients");
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine($"Its {e.GetType()} with message {e.Message}");
                return RedirectToAction("CreateFail");
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Client client = await db.Clients.Include(c => c.Discount).FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (client != null)
                    return View(client);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (client != null)
                {
                    ViewBag.Discounts = new SelectList(db.Discounts, "Discount_percent", "Discount_name");
                    return View(client);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            try
            {
                Console.WriteLine($"lщl{client.Number_of_regular_customers_card} {client.Name}");
                client.Discount = await db.Discounts.FirstOrDefaultAsync(dd => dd.Discount_percent == client.Discount.Discount_percent);
                Console.WriteLine($"3{client.Number_of_regular_customers_card} n={client.Name} d={client.Discount.Discount_percent} {client.Discount.Discount_name}");
                db.Clients.Update(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Clients");
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine($"Its {e.GetType()} with message {e.Message}");
                return RedirectToAction("EditFail");
            }

        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Client client = await db.Clients.Include(c => c.Discount).FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (client != null)
                {
                    return View(client);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (client != null)
                {
                    try
                    {
                        db.Clients.Remove(client);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Clients");
                    }
                    catch (DbUpdateException e)
                    {
                        Console.WriteLine($"Its {e.GetType()} with message {e.Message}");
                        return RedirectToAction("DeleteFail");
                    }

                }
            }
            return NotFound();
        }
    }
}
