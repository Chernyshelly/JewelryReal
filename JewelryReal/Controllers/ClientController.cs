using JewelryReal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationContext db;
        public ClientController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Clients()
        {
            var clients = db.Clients.Include(c => c.Discount);
            return View(await clients.ToListAsync());
        }
        public IActionResult Create()
        {
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
        public async Task<IActionResult> Create(Client user)
        {
            try
            {
                db.Clients.Add(user);
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
                Client user = await db.Clients.Include(c => c.Discount).FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Client user = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Client user)
        {
            try
            {
                db.Clients.Update(user);
                Console.WriteLine($"lщl{user.Number_of_regular_customers_card} {user.Name}");
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
                Client user = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Client user = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == id);
                if (user != null)
                {
                    try
                    {
                        db.Clients.Remove(user);
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
