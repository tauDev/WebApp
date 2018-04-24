using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserDetails
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Employee Id")]
        public string Emplid { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
