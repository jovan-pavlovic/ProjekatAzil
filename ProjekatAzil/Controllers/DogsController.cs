﻿using System;
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
        [AllowAnonymous]
        public ActionResult Index(DogsViewModel viewModelDogs)
        {
            
            IQueryable<Dog> DogQuery = db.Dogs;

            if (viewModelDogs.Wishlist == true && !User.IsInRole(RolesCfg.USER) && !User.IsInRole(RolesCfg.ADMIN))
            {
                return RedirectToAction("Login", "Account");
            }
            if (viewModelDogs.Wishlist.HasValue && viewModelDogs.Wishlist.Value)
            {
                DogQuery = DogQuery.Where(d => d.Users.Any(u => u.UserName == User.Identity.Name));
            }

            if (Request.HttpMethod == "POST")
            {
                viewModelDogs.Page = 1;
            }

            if(viewModelDogs.DogName != null)
            {
                DogQuery = DogQuery.Where(d => d.Name.Contains(viewModelDogs.DogName));
            }
            if(viewModelDogs.DogBreed != null)
            {
                DogQuery = DogQuery.Where(d => d.Breeds.Any(a => a.Name.Contains(viewModelDogs.DogBreed)));
            }
            if (viewModelDogs.DogAge.HasValue)
            {
                DogQuery = DogQuery.Where(d => (DateTime.Now.Year - d.YearOfBirth) <= viewModelDogs.DogAge);
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
        [AllowAnonymous]
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
            ViewBag.BreedCount = db.Breeds.Count();
            return View();
        }

        // POST: Dogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,YearOfBirth,Description,Sex,Weight,Adoption")]Dog dog, int[] dogBreedIds, IEnumerable<HttpPostedFileBase> images)
        {
            ShowBreed();
            if (ModelState.IsValid)
            {

                if (dogBreedIds != null)
                {
                    dog.Breeds = db.Breeds.Where(x => dogBreedIds.Contains(x.Id)).ToList();
                }

                AddImages(dog, images);

                db.Dogs.Add(dog);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Dogs/Edit/5
        public ActionResult Edit(int? id)
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
            ShowBreed();
            ViewBag.BreedCount = db.Breeds.Count();
            return View(dog);
        }

        // POST: Dogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,Name,YearOfBirth,Description,Sex,Weight,Adoption")]Dog dog, int[] dogBreedIds, IEnumerable<HttpPostedFileBase> images)

        {
            ShowBreed();
            if (ModelState.IsValid)
            {

                var dogInDB = db.Dogs.Find(dog.Id);

                TryUpdateModel(dogInDB, new string[] { "Name", "YearOfBirth", "Description", "Sex", "Weight", "Adoption" });

                dogInDB.Breeds.Clear();
                if (dogBreedIds != null)
                {
                    dogInDB.Breeds = db.Breeds.Where(x => dogBreedIds.Contains(x.Id)).ToList();
                }

                AddImages(dogInDB, images);


                db.Entry(dogInDB).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog.Id);
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
            foreach(var image in dog.Images)
            {
                DeleteImage(image.Id);
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

        private void AddImages(Dog dog, IEnumerable<HttpPostedFileBase> images)
        {
            if (dog != null && images.Count() != 0)
            {
                foreach (var item in images)
                {
                    if(item != null)
                    {
                        var image = new Image
                        {
                            NameOfPicture = dog.Id + "_" + Guid.NewGuid() + "_" + item.FileName,
                            Dog = dog
                        };
                        db.Images.Add(image);

                        var imagePath = Server.MapPath($"~/Content/Dog Images/{image.NameOfPicture}");
                        item.SaveAs(imagePath);
                    }
                }
            }
        }

        public ActionResult DeleteImage(int imageId) //qwebfhasdgfyug
        {
            
            var image = db.Images.Find(imageId);
            var dog = image.Dog.Id;
            if (image != null)
            {
                db.Images.Remove(image);
                db.SaveChanges();
                var imagePath = Server.MapPath($"~/Content/Dog Images/{image.NameOfPicture}");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            return RedirectToAction("Edit", dog);
        }


        public ActionResult AddToWishlist(int? id, bool? listMode)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }

            dog.Users.Add(db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name));
            db.SaveChanges();

            return RedirectToAction("Index", new { wishlist = false, listMode });
        }

        public ActionResult RemoveFromWishlist(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }

            dog.Users.Remove(db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name));
            db.SaveChanges();

            return RedirectToAction("Index", new { wishlist = true });
        }


    }
}
