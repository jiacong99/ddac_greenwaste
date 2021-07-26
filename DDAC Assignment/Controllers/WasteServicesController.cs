using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDAC_Assignment.Data;
using DDAC_Assignment.Models;

namespace DDAC_Assignment.Controllers
{
    public class WasteServicesController : Controller
    {
        private readonly DDAC_Context _context;

        public WasteServicesController(DDAC_Context context)
        {
            _context = context;
        }

        // GET: WasteServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.WasteServices.ToListAsync());
        }

        // GET: WasteServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteServices = await _context.WasteServices
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wasteServices == null)
            {
                return NotFound();
            }

            return View(wasteServices);
        }

        // GET: WasteServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WasteServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,servicesTitle,serviceDescription,serviceMediaURL,serviceSize,serviceLimit")] WasteServices wasteServices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wasteServices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wasteServices);
        }

        // GET: WasteServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteServices = await _context.WasteServices.FindAsync(id);
            if (wasteServices == null)
            {
                return NotFound();
            }
            return View(wasteServices);
        }

        // POST: WasteServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,servicesTitle,serviceDescription,serviceMediaURL,serviceSize,serviceLimit")] WasteServices wasteServices)
        {
            if (id != wasteServices.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wasteServices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WasteServicesExists(wasteServices.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wasteServices);
        }

        // GET: WasteServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteServices = await _context.WasteServices
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wasteServices == null)
            {
                return NotFound();
            }

            return View(wasteServices);
        }

        // POST: WasteServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wasteServices = await _context.WasteServices.FindAsync(id);
            _context.WasteServices.Remove(wasteServices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WasteServicesExists(int id)
        {
            return _context.WasteServices.Any(e => e.ID == id);
        }
    }
}
