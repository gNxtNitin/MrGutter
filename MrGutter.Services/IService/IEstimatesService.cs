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
        public Task<int> ChangeEstimateStatus(EstimateVM estimateVM);
        public Task<int> DeleteEstimateAsync(EstimateVM estimateVM);
        public Task<int> UpdateEstimate(EstimateVM estimateIdsVM);
        

        public Task<EstimateStatusVM> GetStatuslistAsync(string? StatusId);
        public Task<EstimateVM> GetEstimatelistAsync(EstimateIdsVM estimateIdsVM);
    }
}
