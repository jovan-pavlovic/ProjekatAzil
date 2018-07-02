using ProjekatAzil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjekatAzil.Controllers
{
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult VolunteerForm(Days[] days)
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

        public ActionResult Donation(DonationType[] donType)
        {

            if (donType != null && donType.Count() > 0)
            {
                var test = (DonationType)donType.Sum(d => (int)d);

                Donation df = new Donation()
                {
                    DonationType = test
                };
            }


            return View("Donation");
        }

        public ActionResult Contact(Form form)
        {
            return View("Contact");
        }


        public ActionResult AdoptionDogForm()
        {
            return View("AdoptionDogForm");
        }
    }
}