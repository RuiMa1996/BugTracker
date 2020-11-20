using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class TicketssController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ticketss
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        // GET: Ticketss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Ticketss/Create
        public ActionResult Create()
        {
            ViewBag.AssignedToUserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.OwnerUserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "id");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "id");
            return View();
        }

        // POST: Ticketss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titile,Description,Created,Updated,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedToUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "id", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "id", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Ticketss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedToUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "id", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "id", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Ticketss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titile,Description,Created,Updated,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.ApplicationUsers, "Id", "Email", ticket.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "id", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "id", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Ticketss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Ticketss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
