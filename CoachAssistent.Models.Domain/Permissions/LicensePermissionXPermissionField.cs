namespace CoachAssistent.Models.Domain.Permissions
{
    public class LicensePermissionXPermissionField
    {
        public int Id { get; set; }
        public int LicensePermissionId { get; set; }
        public int PermissionFieldId { get; set; }

        public LicensePermission? LicensePermission { get; set; }
        public PermissionField? PermissionField { get; set; }
    }
}