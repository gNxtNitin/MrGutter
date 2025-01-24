using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class CompanyVM
    {
    
        public string? CompanyId { get; set; } = "0";


        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? ContactPerson { get; set; }
        public string? CompanyLogo { get; set; }



        public string? Flag { get; set; }
        public bool isActive { get; set; } = false;
        public int? CreatedBy { get; set; } = 0;
        public List<Company>? Company { get; set; }
    }
}
