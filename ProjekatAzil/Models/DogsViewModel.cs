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
        public int? DogAge { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortDirection { get; set; } = "ASC";
        public List<Dog> Dogs { get; set; }
        public List<Event> Events { get; set; }
        public bool? Wishlist { get; set; }
        public bool? ListMode { get; set; }

        public int Count { get; set; }
        public int PageSize { get; set; } = 12;
        public int Page { get; set; } = 1;
        public int TotalPages { get { return (Count + PageSize - 1) / PageSize; } }
        public object Sorting(string Sort)
        {
            var Direction = SortDirection == "ASC" ? "DESC" : "ASC";
            return new
            {
                DogName,
                DogBreed,
                DogAge,
                SortBy = Sort,
                SortDirection = Direction,
                PageSize,
                Page,
                Wishlist,
                ListMode
            };
        }
        public object Pagination(int Page)
        {
            return new
            {
                DogName,
                DogBreed,
                DogAge,
                SortBy,
                SortDirection,
                PageSize,
                Page,
                Wishlist,
                ListMode
            };
        }
    }
}