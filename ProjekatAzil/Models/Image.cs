using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string NameOfPicture { get; set; }

        public virtual Dog Dog { get; set; }
    }
}