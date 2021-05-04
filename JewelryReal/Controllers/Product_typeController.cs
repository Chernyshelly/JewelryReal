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
        public IActionResult CreateFail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product_type product_type)
        {
            try
            {
                db.Product_types.Add(product_type);
                await db.SaveChangesAsync();
                return RedirectToAction("Product_types");
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
                Product_type product_type = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (product_type != null)
                    return View(product_type);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Product_type product_type = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (product_type != null)
                    return View(product_type);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product_type product_type)
        {
            try
            {
                db.Product_types.Update(product_type);
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
                Product_type product_type = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (product_type != null)
                    return View(product_type);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product_type product_type = await db.Product_types.FirstOrDefaultAsync(p => p.TypeID == id);
                if (product_type != null)
                {
                    try
                    {
                        db.Product_types.Remove(product_type);
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
