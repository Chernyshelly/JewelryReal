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
    public class SaleController : Controller
    {
        private ApplicationContext db;
        public SaleController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Sales()
        {
            var sales = db.Sales.Include(c => c.Client).Include(p => p.Product);
            return View(await sales.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(db.Clients, "Number_of_regular_customers_card", "Name");
            ViewBag.Products = new SelectList(db.Products, "ProductID", "Name");
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
        public async Task<IActionResult> Create(Sale sale)
        {
            try
            {
                Console.WriteLine($"{sale.SaleID} p={sale.Product.Name} c={sale.Client.Name} {sale.Client.Surname}");
                sale.Client = await db.Clients.FirstOrDefaultAsync(dd => dd.Number_of_regular_customers_card == sale.Client.Number_of_regular_customers_card);
                sale.Product = await db.Products.FirstOrDefaultAsync(p => p.ProductID == sale.Product.ProductID);
                Console.WriteLine($"2{sale.SaleID} p={sale.Product.Name} c={sale.Client.Name} {sale.Client.Surname}");
                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                return RedirectToAction("Sales");
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
                Sale sale = await db.Sales.Include(c => c.Client).Include(p => p.Product).FirstOrDefaultAsync(s => s.SaleID == id);
                if (sale != null)
                    return View(sale);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Sale sale = await db.Sales.Include(c => c.Client).Include(p => p.Product).FirstOrDefaultAsync(s => s.SaleID == id);
                if (sale != null)
                {
                    Console.WriteLine($"lщl{sale.SaleID} {sale.Product.Name} {sale.Client.Name}");
                    ViewBag.Clients = new SelectList(db.Clients, "Number_of_regular_customers_card", "Name", sale.Client);
                    ViewBag.Products = new SelectList(db.Products, "ProductID", "Name", sale.Product);
                    return View(sale);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Sale sale)
        {
            try
            {
                Console.WriteLine($"lщl{sale.SaleID} {sale.Product.Name} {sale.Client.Name}");
                sale.Product = await db.Products.FirstOrDefaultAsync(p => p.ProductID == sale.Product.ProductID);
                sale.Client = await db.Clients.FirstOrDefaultAsync(p => p.Number_of_regular_customers_card == sale.Client.Number_of_regular_customers_card);
                Console.WriteLine($"3{sale.SaleID} {sale.Product.Name}  {sale.Client.Name}");
                db.Sales.Update(sale);
                await db.SaveChangesAsync();
                return RedirectToAction("Sales");
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
                Sale sale = await db.Sales.Include(c => c.Client).Include(p => p.Product).FirstOrDefaultAsync(s => s.SaleID == id);
                if (sale != null)
                {
                    return View(sale);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Sale sale = await db.Sales.FirstOrDefaultAsync(p => p.SaleID == id);
                if (sale != null)
                {
                    try
                    {
                        db.Sales.Remove(sale);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Sales");
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
