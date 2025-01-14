using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class EstimateStatusVM
    {
        public string? StatusID { get; set; }
        public string? StatusName { get; set; }
        public string? StatusColor { get; set; }
        public List<EstimateStatusVM>? StatusList { get; set; }
    }
}
