using EventEaseAppFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventEaseAppFinal.Controllers
{
    public class EventController : Controller
    {
        private EventEaseDBSContrxt db = new EventEaseDBSContrxt();

        // GET: Event
        public async Task<ActionResult> Index(string searchType, int? venueId, DateTime? startDate, DateTime? endDate)
        {
            var events = db.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .AsQueryable();

            // Filtering based on EventType, Venue, and Date Range
            if (!string.IsNullOrEmpty(searchType))
                events = events.Where(e => e.EventType.Name.Contains(searchType));

            if (venueId.HasValue)
                events = events.Where(e => e.VenueID == venueId);

            if (startDate.HasValue && endDate.HasValue)
                events = events.Where(e => e.EventDate >= startDate && e.EventDate <= endDate);

            // Adding EventTypes and Venues to ViewData for dropdowns
            ViewData["EventTypes"] = db.EventTypes.ToList();
            ViewData["Venues"] = db.Venues.ToList();

            return View(await events.ToListAsync());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "Name");
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName");
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventName,EventDate,Description,VenueID,EventTypeID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "Name", @event.EventTypeID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", @event.VenueID);
            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "Name", @event.EventTypeID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", @event.VenueID);
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventName,EventDate,Description,VenueID,EventTypeID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "Name", @event.EventTypeID);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "VenueName", @event.VenueID);
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}