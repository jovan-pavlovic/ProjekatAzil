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

        public int PhoneNumber { get; set; }
        public string Coment { get; set; }

        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string Address { get; set; }
        public string AnimalType { get; set; }
        public int AgeChildren { get; set; }

        public int DayB { get; set; }
        public int MonB { get; set; }
        public int YearB { get; set; }
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

    [Flags]
    public enum DonationType
    {
        Food = 0,
        Stuff = 1,
        Money = 2,
        Drugs = 4,
        Supplements=8,
        MedicalCare=16
    }

    public enum Yes_No
    {
        Yes,
        No
    }

    public enum Place
    {
        House,
        Apartment,
        Farm,
        Duplex
    }

    public class VolunteerForm : Form
    {
        
        public Services Services { get; set; }
        public Days Days { get; set; } 

    }
    public class Contact : Form
    {

    }
    public class Donation : Form
    {
        public DonationType DonationType { get; set; }
    }

    public class AdoptionDogForm
    {
        public Yes_No Yes_No { get; set; }
        public Place Place { get; set; }

    }
}