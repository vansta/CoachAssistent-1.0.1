﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Permission
{
    public class RolePermissionViewModel
    {
        public RolePermissionViewModel()
        {

        }
        public string? Action { get; set; }
        public string? Subject { get; set; }
        public Guid GroupId { get; set; }
        public IEnumerable<string?> Fields { get; set; } = new List<string?>();
    }
}