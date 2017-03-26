using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string OIB { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string UserRole { get; set; }

        [Required]
        [Display(Name = "Cost Center")]
        public int CostCenterId { get; set; } // id CC-a prosljeđujem u Register.cshtml kao ID za punjenje dropdowna
        public IEnumerable<CostCenter> CostCenters { get; set; } // listu CC-a prosljeđujem u Register.html kao VALUE za punjenje dropdowna
        public CostCenter CostCenter { get; set; } // CC koristim za mapiranje (zbog Details view-a)

        [Required]
        [Display(Name = "Superior")]
        public string SuperiorId { get; set; }
        public IEnumerable<ApplicationUser> Superiors { get; set; }
        public ApplicationUser Superior { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterViewModelEdit
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string OIB { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string UserRole { get; set; }

        [Required]
        [Display(Name = "Cost Center")]
        public int CostCenterId { get; set; } // id CC-a prosljeđujem u Register.cshtml kao ID za punjenje dropdowna
        public IEnumerable<CostCenter> CostCenters { get; set; } // listu CC-a prosljeđujem u Register.html kao VALUE za punjenje dropdowna
        public CostCenter CostCenter { get; set; } // CC koristim za mapiranje (zbog Details view-a)

        [Required]
        [Display(Name = "Superior")]
        public string SuperiorId { get; set; }
        public IEnumerable<ApplicationUser> Superiors { get; set; }
        public ApplicationUser Superior { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
