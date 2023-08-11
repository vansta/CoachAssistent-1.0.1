﻿using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class PermissionManager : BaseAuthenticatedManager
    {
        //readonly IConfiguration configuration;
        public PermissionManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, configuration, authenticationWrapper)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<SelectViewModel> GetActions()
        {
            return dbContext.PermissionActions
                .Select(pa => new SelectViewModel(pa.Id, pa.Name));
        }

        public IEnumerable<PermissionSubjectSelectViewModel> GetSubjects()
        {
            return dbContext.PermissionSubjects
                .Include(ps => ps.Fields)
                .Select(ps => new PermissionSubjectSelectViewModel(ps.Id, ps.Name)
                {
                    Fields = ps.Fields.Select(f => f.Name)
                });
        }
    }
}