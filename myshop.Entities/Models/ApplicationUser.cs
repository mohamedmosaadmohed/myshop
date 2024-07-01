using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(450)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(450)]
        public string LastName { get; set; }
    }
}
