﻿using System;
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
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Coment { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }        
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
        Tuesday = 1,
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

    public class Donation : Form
    {
        public DonationType DonationType { get; set; }
    }

    public class AdoptionDogForm : Form
    {
        public Yes_No AdditionalPets { get; set; }
        public Place Place { get; set; }
        public string AnimalType { get; set; }
        public Yes_No Parent { get; set; }
        public int AgeChildren { get; set; }
        public string DayB { get; set; }

    }
}