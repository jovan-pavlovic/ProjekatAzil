using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class DogsViewModel
    {
        public string DogName { get; set; }
        public string DogBreed { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortDirection { get; set; } = "ASC";
        public List<Dog> Dogs { get; set; }

        public object Sorting(string Sort)
        {
            var Direction = SortDirection == "ASC" ? "DESC" : "ASC";
            return new
            {
                DogName,
                DogBreed,
                SortBy = Sort,
                SortDirection = Direction
            };
        }

    }
}