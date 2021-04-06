using JewelryReal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Controllers
{
    public class MaterialController : Controller
    {
        private ApplicationContext db;
        public MaterialController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Materials()
        {
            return View(await db.Materials.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Material user)
        {
            db.Materials.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Materials");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Material user = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Material user = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Material user)
        {
            db.Materials.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Materials");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Material user = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
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
                Material user = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (user != null)
                {
                    db.Materials.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Materials");
                }
            }
            return NotFound();
        }
    }
}
