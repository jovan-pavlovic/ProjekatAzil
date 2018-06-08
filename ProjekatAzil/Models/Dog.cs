using System;
using System.Collections.Generic;
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

    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public DateTime YearOfBirth { get; set; }
        public int Age { get { return DateTime.Now.Year - YearOfBirth.Year; } }
        public string Description { get; set; }
        public bool Sex { get; set; }
        public decimal Weight { get; set; }
        public AdoptionStatus Adoption { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }


    }
}