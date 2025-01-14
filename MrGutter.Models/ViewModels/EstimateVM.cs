using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class EstimateVM
    {
        public int EstimateID { get; set; } = 0;
        public string? EstimateNo { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNo { get; set; }

        [Required(ErrorMessage = "Address line 1 is required.")]
        [StringLength(100, ErrorMessage = "Address line 1 cannot exceed 100 characters.")]
        public string? Addressline1 { get; set; }
        public string? Addressline2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        //[RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zip code.")]
        public string? ZipCode { get; set; }
        public string? EstimateCreatedDate { get; set; }
        public int CompanyID { get; set; } = 1;
        public int StatusID { get; set; }


        public int CreatedBy { get; set; }
        public List<EstimateVM>? EstimateList { get; set; }
        public List<User>? EstimatorUsers { get; set; }
        public List<EstimateStatusVM>? StatusList { get; set; }
    }

}
