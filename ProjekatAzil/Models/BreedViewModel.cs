using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjekatAzil.Models
{
    public class BreedViewModel
    {
        public string BreedName { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortDirection { get; set; } = "ASC";
        public List<Breed> Breeds { get; set; }

        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int TotalPages { get { return (Count + PageSize - 1) / PageSize; } }
        public object Sorting(string Sort)
        {
            var Direction = SortDirection == "ASC" ? "DESC" : "ASC";
            return new
            {
                BreedName,
                SortBy = Sort,
                SortDirection = Direction,
                PageSize,
                Page,
            };
        }
        public object Pagination(int Page)
        {
            return new
            {
                BreedName,
                SortBy,
                SortDirection,
                PageSize,
                Page,
            };
        }
    }
}