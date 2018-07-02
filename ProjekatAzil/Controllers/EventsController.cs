using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjekatAzil.Models;

namespace ProjekatAzil.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index(int page = 0)
        {
            var events = db.Events.ToList();
            const int pageSize = 3;
            var count = events.Count();
            ViewBag.EventsPage = page;
            ViewBag.EventsTotalPages = (count + pageSize - 1) / pageSize;
            var eventsQuery = events.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(eventsQuery);
            
        }

        // GET: Events/Details/5
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

        // GET: Events/Create
        public ActionResult Create()
        {
            ShowDogs();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Event @event, string DogId, HttpPostedFileBase image)
        {
            //int dog = 0;
            if (ModelState.IsValid)
            {

                if (!DogId.Equals(""))
                {


                    //var dogList = DogId.Split(',')
                    //    .Where(d => int.TryParse(d, out dog))
                    //    .Select(d => int.Parse(d))
                    //    .ToList();
                    var dogList = DogId.Split(',').Select(int.Parse).ToList();
                    @event.Dogs = db.Dogs.Where(x => dogList.Contains(x.Id)).ToList();
                }

                if (@event != null && image != null)
                {
                    AddImage(@event, image);
                }

                
                @event.UploadTimeStamp = DateTime.Now;
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }




        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            ShowDogs();
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

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Event @event,string DogId, HttpPostedFileBase image)
        {
            ShowDogs();
            if (ModelState.IsValid)
            {
                var eventInDB = db.Events.Find(@event.Id);

                TryUpdateModel(eventInDB, new string[] { "Title", "Description" });

                eventInDB.Dogs.Clear();
                if(DogId == "")
                {
                    DogId = null;
                }
                if (DogId != null)
                {
                    var dogList = DogId.Split(',').Select(int.Parse).ToList();
                    eventInDB.Dogs = db.Dogs.Where(x => dogList.Contains(x.Id)).ToList();
                }
                if( image.FileName != null && image.FileName != @event.NameOfImage)
                {
                    if (@event.Image != null)
                    {
                        db.Images.Remove(db.Images.FirstOrDefault(i => @event.Image.Id == i.Id));
                    }
                    AddImage(@event, image);

                    var imagePath = "~/Content/Events Images/" + @event.NameOfImage;
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                db.Entry(eventInDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
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
            if (@event.NameOfImage != "event_default.jpeg")
            {
                db.Images.Remove(db.Images.FirstOrDefault(i => @event.Image.Id == i.Id));
                var imagePath = "~/Content/Events Images/" + @event.Image.NameOfPicture;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
                return View(@event);
        }

        // POST: Events/Delete/5
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


        private void ShowDogs()
        {
            ViewBag.Dogs = db.Dogs.ToList();
        }

        private void AddImage(Event @event, HttpPostedFileBase image)
            {
            var eventImage = new Image
            {
                NameOfPicture = image.FileName,
            };
            db.Images.Add(eventImage);
            db.SaveChanges();
            @event.Image = eventImage;

            var imagePath = Server.MapPath($"~/Content/Event Images/{eventImage.NameOfPicture}");
            image.SaveAs(imagePath);
        }
    }
}
