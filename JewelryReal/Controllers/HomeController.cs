using JewelryReal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Discounts.ToListAsync());
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
        [HttpPost]
        public async Task<IActionResult> Create(Discount user)
        {
            db.Discounts.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Discount user = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Discount user = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Discount user)
        {
            try
            {
                db.Discounts.Update(user);
                Console.WriteLine($"lщl{user.Discount_percent} {user.Discount_name}");
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(DbUpdateException e)
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
                Discount user = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
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
                Discount user = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (user != null)
                {
                    try
                    {
                        db.Discounts.Remove(user);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch(DbUpdateException e)
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
