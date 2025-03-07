using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Data
{
    public static class SeedingLibrary
    {
        public static Guid AdminId { get { return new("5E8876EE-A3F0-4714-9566-22411FAA32D4"); } }
        public static Guid WriterId { get { return new("4948B3BA-6061-4995-AC82-2DA4885839E5"); } }
        public static Guid ReaderId { get { return new("AE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA"); } }
        public static Guid AdminLicenseId { get { return new("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"); } }
        public static Guid DefaultLicenseId { get { return new("ad48ebbb-ae91-457f-b108-6b86d45ad02c"); } }
    }
}
