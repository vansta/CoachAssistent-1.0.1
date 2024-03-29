﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Group
    {
        public Group()
        {
            Name = string.Empty;
            Members = new HashSet<Member>();
            Tags = new HashSet<Tag>();
            ShareablesXGroups = new HashSet<ShareablesXGroups>();
            MembershipRequests = new HashSet<MembershipRequest>();
            SubGroups = new HashSet<Group>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }

        public Guid? ParentGroupId { get; set; }
        public Group? ParentGroup { get; set; }
        public ICollection<Member> Members { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<ShareablesXGroups> ShareablesXGroups { get; set; }
        public ICollection<MembershipRequest> MembershipRequests { get; set; }
        public ICollection<Group> SubGroups { get; set; }
    }
}
