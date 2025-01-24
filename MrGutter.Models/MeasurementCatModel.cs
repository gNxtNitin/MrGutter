using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models
{
    public class MeasurementCatModel
    {
        public int MCatID { get; set; }
        public int CompanyID { get; set; }
        public string? CategoryName { get; set; }
        public string? OrderNo { get; set; }
        public string? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
    }
}
