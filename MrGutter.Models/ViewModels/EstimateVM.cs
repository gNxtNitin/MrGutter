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
        public int UserID { get; set; } = 0;
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
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address line 1 is required.")]
        [StringLength(100, ErrorMessage = "Address line 1 cannot exceed 100 characters.")]
        public string? Addressline1 { get; set; }
        public string? Addressline2 { get; set; }
        public string? NextCallDate { get; set; }
        public string? EstimateRevenue { get; set; }


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
        public string? CompanyName { get; set; }
        public int CompanyID { get; set; } = 1;
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public List<EstimateVM>? EstimateList { get; set; }
        public List<User>? EstimatorUsers { get; set; }
        public List<EstimateStatusVM> StatusList { get; set; }
        public List<MeasurementTokenVM>? MeasurementToken { get; set; }
        public List<MeasurementUnitVM>? MeasurementUnit { get; set; }
        public List<MeasurementCatVM>? MeasurementCat { get; set; }
    }
    public class EstimateIdsVM
    {
        public string? Flag { get; set; }
        public int UserId { get; set; } = 0;
        public int CompanyID { get; set; } = 0;
        public int EstimateID { get; set; } = 0;
        public int StatusID { get; set; } = 0;

    }
    public class CreationStatusFilter
    {
        private DateTime _currentDate;

        public CreationStatusFilter()
        {
            _currentDate = DateTime.Now;
        }

        public DateTime GetStartDate(int creationStatus)
        {
            DateTime startDate;
            if (creationStatus == 24)
            {
                startDate = _currentDate.AddHours(-24);
            }
            else if (creationStatus == 7)
            {
                startDate = _currentDate.AddDays(-7);
            }
            else if (creationStatus == 30)
            {
                startDate = _currentDate.AddDays(-30);
            }
            else if (creationStatus == -1)
            {
                startDate = DateTime.MinValue;
            }
            else
            {
                startDate = DateTime.MinValue;
            }

            return startDate;
        }
    }

}
