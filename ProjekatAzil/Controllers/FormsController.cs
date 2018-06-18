using ProjekatAzil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjekatAzil.Controllers
{
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult Index(Days[] days)
        {

            if (days != null && days.Count() > 0)
            {
                var test = (Days)days.Sum(d => (int)d);

                VolunteerForm vf = new VolunteerForm()
                {
                    Days = test
                };
            }


            return View("VolunteerForm");
        }
    }
}