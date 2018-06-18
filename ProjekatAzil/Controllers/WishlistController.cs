using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjekatAzil.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index()
        {
            return View();
        }

        // GET: Wishlist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Wishlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishlist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wishlist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Wishlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wishlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Wishlist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
