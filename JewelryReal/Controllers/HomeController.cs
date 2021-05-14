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
        public IActionResult CreateFail()
        {
            return View();
        }
        public IActionResult SearchByName()
        {
            ViewBag.FoundDiscounts = db.Discounts;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchByName(string text)
        {
            var foundDiscounts = new List<Discount>();
            foreach (var item in db.Discounts)
            {
                if(item.Discount_name.Contains(text))
                {
                    foundDiscounts.Add(item);
                }
            }
            ViewBag.FoundDiscounts = foundDiscounts;
            return View();
        }

        public IActionResult SearchByPercent()
        {
            ViewBag.FoundDiscounts = db.Discounts;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchByPercent(int percentMin, int percentMax)
        {
            var foundDiscounts = new List<Discount>();
            foreach (var item in db.Discounts)
            {
                if (item.Discount_percent >= percentMin && item.Discount_percent <= percentMax)
                {
                    foundDiscounts.Add(item);
                }
            }
            ViewBag.FoundDiscounts = foundDiscounts;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Discount discount)
        {
            try
            {
                db.Discounts.Add(discount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
                Discount discount = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (discount != null)
                    return View(discount);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Discount discount = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (discount != null)
                    return View(discount);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Discount discount)
        {
            try
            {
                db.Discounts.Update(discount);
                Console.WriteLine($"lщl{discount.Discount_percent} {discount.Discount_name}");
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
                Discount discount = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (discount != null)
                    return View(discount);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Discount discount = await db.Discounts.FirstOrDefaultAsync(p => p.Discount_percent == id);
                if (discount != null)
                {
                    try
                    {
                        db.Discounts.Remove(discount);
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
