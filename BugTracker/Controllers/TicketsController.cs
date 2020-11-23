using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using PagedList.Mvc;
using PagedList;

namespace BugTracker.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index(int? i)
        {
            var tickets = db.Tickets
                .Include(t => t.AssignedToUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .ToList();
            return View(tickets.ToPagedList(i ?? 1, 5));
        }

        [HttpPost]
        public ActionResult Index(int? i, string searchTxt)
        {
            var tickets = db.Tickets
                .Include(t => t.AssignedToUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .ToList();
            if (searchTxt != null)
            {
                tickets = db.Tickets.Where(x => x.Titile.Contains(searchTxt)).ToList();
            }
            return View(tickets.ToPagedList(i ?? 1, 5));
        }

        public ActionResult DeveloperTicketList()
        {
            var currentUserId = User.Identity.GetUserId();
            var tickets = db.Tickets.Include(t => t.AssignedToUser)
                                    .Include(t => t.OwnerUser)
                                    .Include(t => t.Project)
                                    .Include(t => t.TicketPriority)
                                    .Include(t => t.TicketStatus)
                                    .Include(t => t.TicketType);
            var developerTickets = from t in tickets
                                   where t.AssignedToUser.Id == currentUserId
                                   select t;
            return View(developerTickets.ToList());
        }

        public ActionResult SubmitterTicketList()
        {
            var currentUserId = User.Identity.GetUserId();
            var tickets = db.Tickets.Include(t => t.AssignedToUser)
                                    .Include(t => t.OwnerUser)
                                    .Include(t => t.Project)
                                    .Include(t => t.TicketPriority)
                                    .Include(t => t.TicketStatus)
                                    .Include(t => t.TicketType);
            var developerTickets = from t in tickets
                                   where t.OwnerUserId == currentUserId
                                   select t;
            return View(developerTickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UserId = User.Identity.GetUserId();
            if (User.IsInRole("Admin"))
            {
                ViewBag.Identity = "Admin";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var ticketComments = db.TicketComments.Include(t => t.Ticket)
                                    .Include(t => t.User);
            var comments = from c in ticketComments
                           where c.TicketId == id
                           select c;
            ViewBag.Comments = comments;

            var ticketAttachments = db.TicketAttachments.Include(t => t.Ticket).Include(t => t.User);
            var attachments = from a in ticketAttachments
                              where a.TicketId == id
                              select a;
            ViewBag.Attachments = attachments;
            return View(ticket);
        }

        public ActionResult AddAttachment(int TicketId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAttachment(int TicketId, TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                ticketAttachment.UserId = User.Identity.GetUserId();
                ticketAttachment.TicketId = TicketId;
                db.TicketAttachments.Add(ticketAttachment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Titile", ticketComment.TicketId);
            //ViewBag.UserId = User.Identity.GetUserId();
            return View(ticketAttachment);
        }
        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            ViewBag.OwnerUserId = User.Identity.GetUserId();
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Submitter")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titile,Description,Created,Updated,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //ticket.TicketPriorityId = 3;
                //ticket.TicketStatusId = 1;
                ticket.OwnerUserId = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerUserId = User.Identity.GetUserId();
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
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
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
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
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
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

        // POST: Tickets/Delete/5
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
