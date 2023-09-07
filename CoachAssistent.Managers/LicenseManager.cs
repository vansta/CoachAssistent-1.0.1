using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Role;
using CoachAssistent.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoachAssistent.Models.ViewModels.License;

namespace CoachAssistent.Managers
{
    public class LicenseManager : BaseAuthenticatedManager
    {
        public LicenseManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, configuration, authenticationWrapper)
        {
        }

        public OverviewViewModel<LicenseOverviewItemViewModel> GetOverview()
        {
            IQueryable<License> licenses = dbContext.Licenses
                .Include(r => r.LicensePermissions)
                    .ThenInclude(rp => rp.Action)
                .Include(r => r.LicensePermissions)
                    .ThenInclude(rp => rp.Subject)
                .Include(r => r.LicensePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField);

            return new OverviewViewModel<LicenseOverviewItemViewModel>
            {
                Items = licenses.Select(r => mapper.Map <LicenseOverviewItemViewModel>(r))
            };
        }

        public async Task UpdateLicense(LicenseOverviewItemViewModel viewModel)
        {
            License license = await dbContext.Licenses
                .Include(r => r.LicensePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField)
                .SingleAsync(r => r.Id.Equals(viewModel.Id));

            Can("update", "license");
            license.Name = viewModel.Name ?? "New license";
            license.Description = viewModel.Description;
            if (viewModel.LicensePermissions is not null)
            {
                license.LicensePermissions = viewModel.LicensePermissions.Select(x =>
                {
                    LicensePermission? licensePermission = license.LicensePermissions.FirstOrDefault(rp => rp.Id == x.Id);
                    if (licensePermission is null)
                    {
                        licensePermission = new LicensePermission
                        {
                            ActionId = int.TryParse(x.ActionId, out int actionId) ? actionId : 0,
                            SubjectId = int.TryParse(x.SubjectId, out int subjectId) ? subjectId : null,
                            Reason = x.Reason
                        };

                        if (x.Fields is not null)
                        {
                            licensePermission.Fields = x.Fields.Select(f =>
                            {
                                int permissionFieldId = dbContext.PermissionFields.Single(pf => pf.SubjectId == subjectId && pf.Id == int.Parse(f)).Id;
                                return new LicensePermissionXPermissionField
                                {
                                    PermissionFieldId = permissionFieldId
                                };
                            }).ToList();
                        }
                    }
                    else if (x.Fields is not null)
                    {
                        licensePermission.Fields = x.Fields.Select(f =>
                        {
                            LicensePermissionXPermissionField? licensePermissionXPermissionField = licensePermission?.Fields.FirstOrDefault(rppf => rppf.PermissionFieldId == int.Parse(f));

                            return licensePermissionXPermissionField ?? new LicensePermissionXPermissionField { PermissionFieldId = dbContext.PermissionFields.Single(pf => pf.SubjectId == licensePermission!.SubjectId && pf.Id == int.Parse(f)).Id };
                        }).ToList();
                    }
                    return licensePermission;
                }).ToList();
            };

            await dbContext.SaveChangesAsync();
        }
    }
}
