using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Dog> Dogs { get; set; }

    }
}