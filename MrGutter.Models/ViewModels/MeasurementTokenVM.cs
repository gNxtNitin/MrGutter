using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class MeasurementTokenVM
    {
        public string? MTokenID { get; set; }
        public string? EstimateId { get; set; }
        public string? CompanyID { get; set; }
        public string? MCatID { get; set; }
        public string? UMID { get; set; }
        public string? TokenName { get; set; }
        public string? TokenValue { get; set; }
        public string? OrderNo { get; set; }
        public string? MeasurementTokenList { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public List<MeasurementTokenVM>? MeasurementToken { get; set; }

    }

   

}
