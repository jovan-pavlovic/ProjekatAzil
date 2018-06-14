using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjekatAzil.Models;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Net;

namespace ProjekatAzil.Controllers
{
    public class DogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dogs
        public ActionResult Index(DogsViewModel viewModelDogs)
        {
            //ShowBreed();
            IQueryable<Dog> DogQuery = db.Dogs;
            if(viewModelDogs.DogName != null)
            {
                DogQuery = DogQuery.Where(d => d.Name.Contains(viewModelDogs.DogName));
            }
            if(viewModelDogs.DogBreed != null)
            {
                DogQuery = DogQuery.Where(d => d.Breeds.Any(a => a.Name.Contains(viewModelDogs.DogBreed)));
            }
            if(viewModelDogs.SortBy != null && viewModelDogs.SortDirection != null)
            {
                DogQuery = DogQuery.OrderBy(string.Format("{0} {1}", viewModelDogs.SortBy, viewModelDogs.SortDirection));
            }

            viewModelDogs.Count = DogQuery.Count();
            DogQuery = DogQuery.Skip((viewModelDogs.Page - 1) * viewModelDogs.PageSize).Take(viewModelDogs.PageSize);

            viewModelDogs.Dogs = DogQuery.ToList();


            
            return View(viewModelDogs);
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

                dog.Breeds = db.Breeds.Where(x => dogBreedIds.Contains(x.Id)).ToList();

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
        public ActionResult Edit(Dog dog, int[] dogBreedIds)
        {
            ShowBreed();
            if (ModelState.IsValid)
            {
               
                var dogBase = db.Dogs.Find(dog.Id);
                TryUpdateModel(dogBase, new string[] { "Name", "YearOfBirth", "Description", "Sex", "Weight", "Adoption" });
                dogBase.Breeds.Clear();
                if(dogBreedIds != null)
                {
                    dogBase.Breeds = db.Breeds.Where(x => dogBreedIds.Contains(x.Id)).ToList();
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
    }
}
