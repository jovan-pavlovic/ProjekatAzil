using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class Form
    {
       public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Coment { get; set; }
    }
    public enum Services
    {
        [Display(Name ="gjuuk")]
        DogWalking,
        DogCare,
        ShelterMaintenance,
        Anything
    }

    [Flags]
    public enum Days
    {
        Monday = 0,
        Thuseday = 1,
        Wednesday = 2,
        Thursday = 4,
        Friday = 8,
        Saturday = 16,
        Sunday = 32
    }
    

   

    public class VolunteerForm : Form
    {
        
        public Services Services { get; set; }
        public Days Days { get; set; } 

    }
}