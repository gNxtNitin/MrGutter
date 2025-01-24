using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class MeasurementCatVM
    {
        public string? MCatID { get; set; }
        public string? CompanyID { get; set; }
        public string? CategoryName { get; set; }
        public string? OrderNo { get; set; }
        public string? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public List<MeasurementCatVM>? MeasurementCat { get; set; }

    }
    
    
}
