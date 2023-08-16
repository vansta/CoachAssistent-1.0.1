﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid LicenseId { get; set; }
        public ICollection<Guid> Memberships { get; set; } = new HashSet<Guid>();
    }
}
