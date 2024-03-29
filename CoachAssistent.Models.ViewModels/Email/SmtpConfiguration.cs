﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Email
{
    public class SmtpConfiguration
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
