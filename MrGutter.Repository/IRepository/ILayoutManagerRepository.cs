using MrGutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository.IRepository
{
    public interface ILayoutManagerRepository
    {
        public Task<APIResponseModel> CreateLayoutAsync(CreateReportLayoutModel createReportLayoutModel);
    }
}
