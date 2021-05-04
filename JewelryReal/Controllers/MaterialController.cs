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
        public IActionResult DeleteFail()
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
                {
                    Console.WriteLine($"id={id} and userId={user.MaterialID}");
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int materialId, Material user)
        {
            Console.WriteLine($"lul{materialId}");
            user.MaterialID = materialId;
            Console.WriteLine($"lщl{user.MaterialID}");
            db.Materials.Update(user);
            Console.WriteLine(user.MaterialID);
            await db.SaveChangesAsync();
            Console.WriteLine(user.MaterialID);
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
                    try
                    {
                        db.Materials.Remove(user);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Materials");
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
