using MrGutter.Models.ViewModels;
using MrGutter.Models;
using MrGutter.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrGutter.Repository.IRepository;

namespace MrGutter.Services
{
    public class LayoutManagerService : ILayoutManagerService
    {
        private readonly ILayoutManagerRepository _layoutManagerRepository;
        public LayoutManagerService(ILayoutManagerRepository layoutManagerRepository)
        {
            _layoutManagerRepository = layoutManagerRepository;
        }
        public async Task<int> CreateLayoutAsync(CreateReportLayoutVM createReportLayoutVM)
        {
            int result = 0;
            CreateReportLayoutModel model = new CreateReportLayoutModel();
            model.LayoutName = createReportLayoutVM.LayoutName;
            model.IsShared = createReportLayoutVM.IsShared;
            model.LayoutCreate = createReportLayoutVM.LayoutCreate;

            var response = await _layoutManagerRepository.CreateLayoutAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;

        }
    }
}
