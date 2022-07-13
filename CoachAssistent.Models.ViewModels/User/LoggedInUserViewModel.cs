﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class LoggedInUserViewModel
    {
        public LoggedInUserViewModel()
        {
            UserName = string.Empty;
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
    }
}
