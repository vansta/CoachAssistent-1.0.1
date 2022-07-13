using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            UserName = string.Empty;
            PasswordHash = string.Empty;
        }
        [Required]
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
