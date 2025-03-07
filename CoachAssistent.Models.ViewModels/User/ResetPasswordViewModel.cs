using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? PasswordHash { get; set; }
    }
}
