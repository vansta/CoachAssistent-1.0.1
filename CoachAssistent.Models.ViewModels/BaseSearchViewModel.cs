using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels
{
    public class BaseSearchViewModel
    {
        public bool? OnlyFavorites { get; set; }
        public bool? OnlyOwned { get; set; }
        public bool? OnlyVerified { get; set; }
        public string? Level { get; set; }
        public string? Search { get; set; }
        public IList<string?>? Tags { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }

        public bool ShowAll { get { return ItemsPerPage <= 0; } }
        public int Skip { get { return (CurrentPage - 1) * ItemsPerPage; } }
    }
}
