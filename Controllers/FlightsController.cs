using FlightsList.Data;
using FlightsList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightsList.Controllers
{
    // Slug style route for the grid page
    [Route("flights-list")]
    public class FlightsController : Controller
    {
        private readonly FlightsContext _context;

        // Pre-defined cities for dropdown
        private static readonly string[] Cities =
        {
            "Chicago", "New York", "Dubai", "London", "Hong Kong", "San Francisco"
        };

        public FlightsController(FlightsContext context)
        {
            _context = context;
        }

        private void LoadCities(string? selectedFrom = null, string? selectedTo = null)
        {
            ViewBag.FromCities = new SelectList(Cities, selectedFrom);
            ViewBag.ToCities = new SelectList(Cities, selectedTo);
        }

        // GET: /flights-list/
        [HttpGet("")]
        public IActionResult Index()
        {
            var flights = _context.Flights
                .OrderBy(f => f.Date)
                .ToList();

            return View(flights);
        }

        // GET: /flights-list/add/
        [HttpGet("add")]
        public IActionResult Upsert()
        {
            LoadCities();
            return View("Upsert", new Flight { Date = DateTime.Today });
        }

        // POST: /flights-list/add/
        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Flight flight)
        {
            if (flight.From == flight.To)
            {
                ModelState.AddModelError(string.Empty, "From and To cannot be the same city.");
            }

            if (!ModelState.IsValid)
            {
                LoadCities(flight.From, flight.To);
                return View("Upsert", flight);
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /flights-list/edit/5/
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null) return NotFound();

            LoadCities(flight.From, flight.To);
            return View("Upsert", flight); // shared view
        }

        // POST: /flights-list/edit/5/
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Flight posted)
        {
            var dbFlight = _context.Flights.Find(id);
            if (dbFlight == null) return NotFound();

            // FlightNumber must NOT be editable
            // Overwrite posted FlightNumber with DB value
            posted.FlightNumber = dbFlight.FlightNumber;

            if (posted.From == posted.To)
            {
                ModelState.AddModelError(string.Empty, "From and To cannot be the same city.");
            }

            if (!ModelState.IsValid)
            {
                LoadCities(posted.From, posted.To);
                return View("Upsert", posted);
            }

            // Update only allowed fields
            dbFlight.From = posted.From;
            dbFlight.To = posted.To;
            dbFlight.Date = posted.Date;
            dbFlight.Price = posted.Price;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /flights-list/delete/5/
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null) return NotFound();
            return View(flight); // confirmation page (no JS)
        }

        // POST: /flights-list/delete/5/
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null) return NotFound();

            _context.Flights.Remove(flight);
            _context.SaveChanges();

            // After delete user sees it removed from grid (Index reloads)
            return RedirectToAction(nameof(Index));
        }
    }
}
