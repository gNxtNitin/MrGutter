using MrGutter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services.IService
{
    public interface ILayoutManagerService
    {
        public Task<int> CreateLayoutAsync(CreateReportLayoutVM createReportVM);
    }
}
