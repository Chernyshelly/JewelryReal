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
        public IActionResult EditFail()
        {
            return View();
        }
        public IActionResult CreateFail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            try
            {
                db.Materials.Add(material);
                await db.SaveChangesAsync();
                return RedirectToAction("Materials");
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
                Material material = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (material != null)
                    return View(material);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Material material = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (material != null)
                {
                    Console.WriteLine($"id={id} and materialId={material.MaterialID}");
                    return View(material);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int materialId, Material material)
        {
            try
            {
                Console.WriteLine($"lul{materialId}");
                material.MaterialID = materialId;
                Console.WriteLine($"lщl{material.MaterialID}");
                db.Materials.Update(material);
                Console.WriteLine(material.MaterialID);
                await db.SaveChangesAsync();
                Console.WriteLine(material.MaterialID);
                return RedirectToAction("Materials");
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
                Material material = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (material != null)
                    return View(material);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Material material = await db.Materials.FirstOrDefaultAsync(p => p.MaterialID == id);
                if (material != null)
                {
                    try
                    {
                        db.Materials.Remove(material);
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
