using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Dog> Dogs { get; set; }
    }
}