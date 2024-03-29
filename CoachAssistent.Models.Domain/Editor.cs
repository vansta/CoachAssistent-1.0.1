﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Editor
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ShareableId { get; set; }

        public User? User { get; set; }

        public Shareable? Shareable { get; set; }
    }
}
