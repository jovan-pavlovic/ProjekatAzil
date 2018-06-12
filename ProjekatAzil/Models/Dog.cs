using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public enum AdoptionStatus
    {
        FreeForAdoption,
        AdoptionPending,
        Adopted
    }

    public enum Sex
    {
        Male,
        Female
    }

    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set;}
        //[Range(2000,2018)] moze i ovo ali sa JS izgleda bolje,vise koda
        public int YearOfBirth { get; set; }
        public int Age { get { return DateTime.Now.Year - YearOfBirth; } }
        public string Description { get; set; }
        public Sex Sex { get; set; }
        public decimal Weight { get; set; }
        public AdoptionStatus Adoption { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }


    }
}