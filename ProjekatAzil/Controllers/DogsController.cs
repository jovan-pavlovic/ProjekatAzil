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
    public class DogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dogs
        public ActionResult Index()
        {
            ShowBreed();
            return View(db.Dogs.ToList());
        }

        // GET: Dogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // GET: Dogs/Create
        public ActionResult Create()
        {
            ShowBreed();
            return View();
        }

        // POST: Dogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,YearOfBirth,Description,Sex,Weight,Adoption")] Dog dog, int[] dogBreedIds)
        {
            ShowBreed();
            if (ModelState.IsValid)
            {
                dog.Adoption = AdoptionStatus.FreeForAdoption;

                InputBreedsForDog(dog, dogBreedIds);

                db.Dogs.Add(dog);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dog);
        }

        // GET: Dogs/Edit/5
        public ActionResult Edit(int? id)
        {
            ShowBreed();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: Dogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,YearOfBirth,Description,Sex,Weight,Adoption")] Dog dog, int[] dogBreedIds)
        {
            ShowBreed();
            if (ModelState.IsValid)
            {
                var dogBase = db.Dogs.Find(dog.Id);

                if (dogBase.Breeds.Count() != 0)
                {
                    //breeds that are in dog.Breeds but not checked in view
                    dogBase.Breeds.Where(b => !dogBreedIds.Contains(b.Id)).ToList().ForEach(b => dogBase.Breeds.Remove(b));
                }
                else
                {
                    InputBreedsForDog(dogBase, dogBreedIds);
                }

                db.Entry(dogBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog);
        }

        // GET: Dogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: Dogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dog dog = db.Dogs.Find(id);
            db.Dogs.Remove(dog);
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


        private void ShowBreed()
        {
            ViewBag.Breed = db.Breeds.ToList();
        }

        private void InputBreedsForDog(Dog dog, int[] dogBreedIds)
        {
            foreach(var breedId in dogBreedIds)
            {
                var breed = db.Breeds.FirstOrDefault(b => b.Id == breedId);
                db.Dogs.FirstOrDefault(d => d.Id == dog.Id).Breeds.Add(breed);
            }
        }
    }
}
