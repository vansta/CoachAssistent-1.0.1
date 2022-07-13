using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class CredentialsViewModel
    {
        public CredentialsViewModel()
        {
            UserName = string.Empty;
            PasswordHash = string.Empty;
        }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
