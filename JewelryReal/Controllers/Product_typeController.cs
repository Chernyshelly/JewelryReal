using JewelryReal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Controllers
{
    public class Product_typeController : Controller
    {
        private ApplicationContext db;
        public Product_typeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Product_types()
        {
            return View(await db.Product_types.ToListAsync());
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
        public async Task<IActionResult> Create(Product_type user)
        {
            db.Product_types.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Product_types");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Product_type user = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Product_type user = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product_type user)
        {
            try
            {
                db.Product_types.Update(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Product_types");
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
                Product_type user = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
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
                Product_type user = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (user != null)
                {
                    try
                    {
                        db.Product_types.Remove(user);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Product_types");
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
