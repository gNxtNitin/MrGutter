using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class CreateReportLayoutVM
    {
        public string? LayoutName {  get; set; }
        public Boolean? IsShared { get; set; }
        public string? LayoutCreate { get; set; } = "Quote";

    }
}
