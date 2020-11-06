using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBoard.Models;

namespace ProjectBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ATaskController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ATask
        // when authorizing certain developers to view one's own task, do the following...
        // [Authorize(Users = "*", Roles = "*")]
        // might have to customize authorization: do some research
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(a => a.Project);
            return View(tasks.ToList());
        }

        // GET: ATask/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATask aTask = db.Tasks.Find(id);
            if (aTask == null)
            {
                return HttpNotFound();
            }
            return View(aTask);
        }*/

        // GET: ATask/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: ATask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProjectId,Body,StartDate,Deadline,IsCompleted,CompletionPerc,Priority")] ATask aTask)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(aTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", aTask.ProjectId);
            return View(aTask);
        }

        // GET: ATask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATask aTask = db.Tasks.Find(id);
            if (aTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", aTask.ProjectId);
            return View(aTask);
        }

        // POST: ATask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProjectId,Body,StartDate,Deadline,IsCompleted,CompletionPerc,Priority")] ATask aTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aTask).State = EntityState.Modified;
                // aTask.CompleteTurnsPerc100();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", aTask.ProjectId);
            return View(aTask);
        }

        // GET: ATask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATask aTask = db.Tasks.Find(id);
            if (aTask == null)
            {
                return HttpNotFound();
            }
            return View(aTask);
        }

        // POST: ATask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ATask aTask = db.Tasks.Find(id);
            db.Tasks.Remove(aTask);
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
