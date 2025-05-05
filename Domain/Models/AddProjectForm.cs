using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AddProjectForm
    {
        [Display(Name = "Project Image", Prompt = "Select a image")]
        [DataType(DataType.Upload)]
        public IFormFile? ProjectImage { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Project Name", Prompt = "Enter project name")]
        [Required(ErrorMessage = "Required")]
        public string ProjectName { get; set; } = null!;


        [DataType(DataType.Text)]
        [Display(Name = "Client Name", Prompt = "Enter Client  name")]
        [Required(ErrorMessage = "Required")]
        public string ClientName { get; set; } = null!;


        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", Prompt = " description")]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "StartDate", Prompt = " Start Date")]
        [Required(ErrorMessage = "Required")]
        public string StartDate { get; set; } = null!;


        [DataType(DataType.Date)]
        [Display(Name = "EndDate", Prompt = " End Date")]
        [Required(ErrorMessage = "Required")]
        public string EndDate { get; set; } = null!;


        [DataType(DataType.Text)]
        [Display(Name = "Member Name", Prompt = "Enter Member  name")]
        [Required(ErrorMessage = "Required")]
        public string MemberName { get; set; } = null!;


        [DataType(DataType.Currency)]
        [Display(Name = "Budget", Prompt = "$ 0")]
        [Required(ErrorMessage = "Required")]
        public string Budget { get; set; } = null!;
    }
}
