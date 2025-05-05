using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EditMemberForm
    {
        public int Id { get; set; }

        [Display(Name = "Member Image", Prompt = "Select a image")]
        [DataType(DataType.Upload)]
        public IFormFile? MemberImage { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Member Name", Prompt = "Enter Member  name")]
        [Required(ErrorMessage = "Required")]
        public string MemberName { get; set; } = null!;

        [Display(Name = "Email", Prompt = "Enter email address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone", Prompt = "Enter phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Location", Prompt = "Enter location")]
        [DataType(DataType.Text)]
        public string? Location { get; set; }


        [Display(Name = "Address", Prompt = "Enter address")]
        [DataType(DataType.Text)]
        public string? Address { get; set; }


        [Required(ErrorMessage = "Day is required")]
        [Range(1, 31, ErrorMessage = "Invalid day")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Month is required")]
        [Range(1, 12, ErrorMessage = "Invalid month")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Invalid year")]
        public int Year { get; set; }

    }

}
