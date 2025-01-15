using MrGutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository.IRepository
{
    public interface IEstimatesRepository
    {
        public Task<APIResponseModel> CreateEstimateAsync(EstimateModel estimateModel);
        public Task<APIResponseModel> GetEstimatelistAsync(EstimateIdsModel estimateIdsModel);
       public Task<APIResponseModel> GetStatuslistAsync(string? EstimateId);
        public Task<APIResponseModel> ChangeEstimateStatus(EstimateModel estimateModel);
        public Task<APIResponseModel> DeleteEstimateAsync(EstimateModel estimateModel);
        public Task<APIResponseModel> UpdateEstimate(EstimateModel estimateModel);

        
    }
}
