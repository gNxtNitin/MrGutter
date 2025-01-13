using MrGutter.Models.ViewModels;
using MrGutter.Models;
using MrGutter.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrGutter.Repository.IRepository;
using MrGutter.Repository;
using Newtonsoft.Json;

namespace MrGutter.Services
{
    public class EstimatesService : IEstimatesService
    {
        private readonly IEstimatesRepository _estimatesRepository;
        public  EstimatesService(IEstimatesRepository estimatesRepository)
        {
            _estimatesRepository = estimatesRepository;

        }
        public async Task<int> CreateEstimateAsync(EstimateVM estimateVM)
        {
            int result = 0;
           EstimateModel model =  new EstimateModel();
            model.EstimateNo =  estimateVM.EstimateNo;
            model.FirstName = estimateVM.FirstName;
            model.LastName = estimateVM.LastName;
            model.Company = estimateVM.Company;
            model.Email = estimateVM.Email;
            model.PhoneNo = estimateVM.PhoneNo;
            model.Addressline1 = estimateVM.Addressline1;
            model.Addressline2 = estimateVM.Addressline2;
            model.City = estimateVM.City;
            model.State = estimateVM.State;
            model.ZipCode = estimateVM.ZipCode;
            model.CompanyID = estimateVM.CompanyID;
            model.CreatedBy = 1;
            model.Flag = "C";
            var response = await _estimatesRepository.CreateEstimateAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<EstimateVM> GetEstimatelistAsync(string? EstimateId)
        {
            EstimateVM estimates = new EstimateVM();
            var response = await _estimatesRepository.GetEstimatelistAsync(EstimateId);
            if (response.Data != null)
            {
                estimates = JsonConvert.DeserializeObject<EstimateVM>(response.Data);
            }
            return estimates;
        }
    }
}
