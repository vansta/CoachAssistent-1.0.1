﻿using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Data.Migrations;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class RoleManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : BaseAuthenticatedManager(context, mapper, configuration, authenticationWrapper)
    {
        public IEnumerable<SelectViewModel> GetRoles()
        {
            return dbContext.Roles.Select(r => new SelectViewModel(r.Id, r.Name));
        }

        public OverviewViewModel<RoleOverviewItemViewModel> GetRolesOverview()
        {
            IQueryable<Role> roles = dbContext.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Action)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Subject)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField)
                .Where(r => r.MinimalLicenseLevel <= authenticationWrapper.LicenseLevel)
                .OrderBy(r => r.Index);

            return new OverviewViewModel<RoleOverviewItemViewModel>
            {
                Items = roles.Select(r => mapper.Map<RoleOverviewItemViewModel>(r))
            };
        }

        public async Task UpdateRole(RoleOverviewItemViewModel roleViewModel)
        {
            Role role = await dbContext.Roles
                .Include(r => r.RolePermissions)
                    //.ThenInclude(rp => rp.Action)
                .Include(r => r.RolePermissions)
                    //.ThenInclude(rp => rp.Subject)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField)
                .SingleAsync(r => r.Id.Equals(roleViewModel.Id));

            Can("update", "role");
            role.Name = roleViewModel.Name ?? "New role";
            role.Description = roleViewModel.Description;
            if (roleViewModel.RolePermissions is not null)
            {
                role.RolePermissions = roleViewModel.RolePermissions.Select(x =>
                {
                    RolePermission? rolePermission = role.RolePermissions.FirstOrDefault(rp => rp.Id == x.Id);
                    if (rolePermission is null)
                    {
                        rolePermission = new RolePermission
                        {
                            ActionId = int.TryParse(x.ActionId, out int actionId) ? actionId : 0,
                            SubjectId = int.TryParse(x.SubjectId, out int subjectId) ? subjectId : null,
                            Reason = x.Reason
                        };

                        if (x.Fields is not null)
                        {
                            rolePermission.Fields = x.Fields.Select(f =>
                            {
                                int permissionFieldId = dbContext.PermissionFields.Single(pf => pf.SubjectId == subjectId && pf.Id == int.Parse(f)).Id;
                                return new RolePermissionXPermissionField
                                {
                                    PermissionFieldId = permissionFieldId
                                };
                            }).ToList();
                        }
                    }
                    else if (x.Fields is not null)
                    {
                        rolePermission.Fields = x.Fields.Select(f =>
                        {
                            RolePermissionXPermissionField? rolePermissionXPermissionField = rolePermission?.Fields.FirstOrDefault(rppf => rppf.PermissionFieldId == int.Parse(f));

                            return rolePermissionXPermissionField ?? new RolePermissionXPermissionField { PermissionFieldId = dbContext.PermissionFields.Single(pf => pf.SubjectId == rolePermission!.SubjectId && pf.Id == int.Parse(f)).Id };
                        }).ToList();
                    }
                    return rolePermission;
                }).ToList();
            };

            await dbContext.SaveChangesAsync();
        }
    }
}
