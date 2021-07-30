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
    public class BookingsController : Controller
    {
        private readonly DDAC_Context _context;

        public BookingsController(DDAC_Context context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Booking.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            List<WasteServices> bookingtypelist = new List<WasteServices>();
            bookingtypelist = (from product in _context.WasteServices select product).ToList();
            bookingtypelist.Insert(0, new WasteServices { ID = 0, servicesTitle = "Select Booking Type" });

            ViewBag.bookingtypelist = bookingtypelist;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,BookingType,BookingPrice,BookingDate,BookingLocation,BookingStatus,DriverName")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<WasteServices> bookingtypelist = new List<WasteServices>();
            bookingtypelist = (from product in _context.WasteServices select product).ToList();
            bookingtypelist.Insert(0, new WasteServices { ID = 0, servicesTitle = "Select Booking Type" });

            ViewBag.bookingtypelist = bookingtypelist;
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            List<WasteServices> bookingtypelist = new List<WasteServices>();
            bookingtypelist = (from product in _context.WasteServices select product).ToList();
            bookingtypelist.Insert(0, new WasteServices { ID = 0, servicesTitle = "Select Booking Type" });

            ViewBag.bookingtypelist = bookingtypelist;
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,BookingType,BookingPrice,BookingDate,BookingLocation,BookingStatus,DriverName")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if(booking.BookingType == "0")
            {
                ModelState.AddModelError("", "Please Select Service Type");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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

            List<WasteServices> bookingtypelist = new List<WasteServices>();
            bookingtypelist = (from product in _context.WasteServices select product).ToList();
            bookingtypelist.Insert(0, new WasteServices { ID = 0, servicesTitle = "Select Booking Type" });

            ViewBag.bookingtypelist = bookingtypelist;
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }

        public JsonResult ajaxGetPriceByService(String name)
        {
            return Json(_context.WasteServices.Where(e => e.servicesTitle == name).FirstOrDefault());
        }
    }
}
