using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TRXpense.Bll.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(11)]
        public string OIB { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string UserRole { get; set; }

        public int CostCenterId { get; set; } // foreign key
        public CostCenter CostCenter { get; set; }

        public string SuperiorId { get; set; } // foreign key
        public ApplicationUser Superior { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
