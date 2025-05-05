

using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class MemberSignUpForm
{
    [Required]
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = null!;
    [Required]
    [Display(Name = "Email", Prompt = "Enter your email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    
    [Display(Name = "Phone", Prompt = "Enter your phone number")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;
    [Required]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Compare(nameof(Password),ErrorMessage ="Password don't match!")]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Terms & Conditions ", Prompt = "I accept the terms & conditions.")]

    public bool TermsAndConditions { get; set; }

}
