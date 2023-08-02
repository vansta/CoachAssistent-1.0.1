using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels
{
    public class SelectViewModel
    {
        public SelectViewModel(Guid id, string? title)
        {
            Value = id.ToString();
            Title = title;
        }
        public SelectViewModel(int id, string? title)
        {
            Value = id.ToString();
            Title = title;
        }
        public SelectViewModel(string? value, string? title)
        {
            Value = value;
            Title = title;
        }
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
