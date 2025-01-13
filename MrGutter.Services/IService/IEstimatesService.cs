using MrGutter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services.IService
{
    public interface IEstimatesService
    {
        public Task<int> CreateEstimateAsync(EstimateVM estimateVM);
        public Task<EstimateVM> GetEstimatelistAsync(string? EstimateId);
    }
}
