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
        public string? Search { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
