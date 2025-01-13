using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models
{
    public class EstimateModel
    {
        public string? Flag { get; set; }
        public int EstimateID { get; set; } = 0;
        public string? EstimateNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? Addressline1 { get; set; }
        public string? Addressline2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public bool IsShared { get; set; }
        public int CompanyID { get; set; }
        public int CreatedBy { get; set; }
    }
}
