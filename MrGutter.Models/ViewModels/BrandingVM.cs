using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class BrandingVM
    {
        public string? AccountName { get; set; }
        public string? BusinessNumber { get; set; }
        public string? WebAddress { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public IFormFile? CompanyLogo { get; set; }
        public string? CompanyLogoPath { get; set; }
        public List<ThemeModel>? ThemeList { get; set; }
        public List<BrandColorModel>? BrandColorList { get; set; }
    }

    public class ThemeModel
    {
        public string? ThemeId { get; set; }
        public string? ThemePath { get; set; }
        public bool? IsActive { get; set; }
    }

    public class BrandColorModel
    {
        public string? Primary { get; set; }
        public string? Secondary { get; set; }
        public bool? IsActive { get; set; }
    }


}
