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
using Microsoft.AspNetCore.Http;

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
            model.PhoneNo = estimateVM.PhoneNumber;
            model.Addressline1 = estimateVM.Addressline1;
            model.Addressline2 = estimateVM.Addressline2;
            model.City = estimateVM.City;
            model.State = estimateVM.State;
            model.ZipCode = estimateVM.ZipCode;
            model.CompanyID = estimateVM.CompanyID;
            model.CreatedBy = estimateVM.CreatedBy;
            model.Flag = "C";
            var response = await _estimatesRepository.CreateEstimateAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<int> UpdateEstimate(EstimateVM estimateVM)
        {
            int result = 0;

            EstimateModel model = new EstimateModel();
            //model.EstimateNo = estimateVM.EstimateList[0].EstimateNo;
            model.EstimateNo = estimateVM.EstimateList[0].EstimateNo;
            model.FirstName = estimateVM.EstimateList[0].FirstName;
            model.LastName = estimateVM.EstimateList[0].LastName;
            model.Company = estimateVM.EstimateList[0].CompanyName;
            model.Email = estimateVM.EstimateList[0].Email;
            model.PhoneNo = estimateVM.EstimateList[0].PhoneNumber;
            model.Addressline1 = estimateVM.EstimateList[0].Addressline1;
            model.Addressline2 = estimateVM.EstimateList[0].Addressline2;
            model.City = estimateVM.EstimateList[0].City;
            model.State = estimateVM.EstimateList[0].State;
            model.ZipCode = estimateVM.EstimateList[0].ZipCode;
            model.NextCallDate = estimateVM.EstimateList[0].NextCallDate;
            model.EstimateRevenue = estimateVM.EstimateList[0].EstimateRevenue;
            model.CompanyID = estimateVM.EstimateList[0].CompanyID;
            model.CreatedBy = estimateVM.EstimateList[0].CreatedBy;
            model.EstimateCreatedDate = estimateVM.EstimateList[0].EstimateCreatedDate;
            model.EstimateID = estimateVM.EstimateList[0].EstimateID;
            model.UserID = estimateVM.EstimateList[0].UserID;
            model.Flag = "U";
            var response = await _estimatesRepository.UpdateEstimate(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        
        public async Task<EstimateVM> GetEstimatelistAsync(EstimateIdsVM estimateIdsVM)
        {
            EstimateVM estimates = new EstimateVM();
            EstimateIdsModel estimateIdsModel = new EstimateIdsModel();
            estimateIdsModel.UserId = estimateIdsVM.UserId;
            estimateIdsModel.CompanyID = estimateIdsVM.CompanyID;
            estimateIdsModel.EstimateID = estimateIdsVM.EstimateID;

            var response = await _estimatesRepository.GetEstimatelistAsync(estimateIdsModel);
            if (response.Data != null)
            {
                estimates = JsonConvert.DeserializeObject<EstimateVM>(response.Data);
            }
            return estimates;
        }
        public async Task<EstimateStatusVM> GetStatuslistAsync(string? StatusId)
        {
            int status = 0;
            EstimateStatusVM estimateStatusVM = new EstimateStatusVM();
            var response = await _estimatesRepository.GetStatuslistAsync(StatusId);
            if (response.Data != null)
            {
                estimateStatusVM = JsonConvert.DeserializeObject<EstimateStatusVM>(response.Data);
            }
            return estimateStatusVM;
        }
        public async Task<int> ChangeEstimateStatus(EstimateVM estimateVM)
        {
            int result = 0;
            EstimateModel model = new EstimateModel();
            model.EstimateNo = estimateVM.EstimateNo;
            model.FirstName = estimateVM.FirstName;
            model.LastName = estimateVM.LastName;
            model.Company = estimateVM.Company;
            model.Email = estimateVM.Email;
            model.PhoneNo = estimateVM.PhoneNumber;
            model.Addressline1 = estimateVM.Addressline1;
            model.Addressline2 = estimateVM.Addressline2;
            model.City = estimateVM.City;
            model.State = estimateVM.State;
            model.ZipCode = estimateVM.ZipCode;
            model.CompanyID = estimateVM.CompanyID;
            model.EstimateID = estimateVM.EstimateID;
            model.StatusID = estimateVM.StatusID;
            model.CreatedBy = 1;
            model.Flag = "S";
            var response = await _estimatesRepository.ChangeEstimateStatus(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<int> DeleteEstimateAsync(EstimateVM estimateVM)
        {
            int result = 0;
            EstimateModel model = new EstimateModel();
            model.EstimateNo = estimateVM.EstimateNo;
            model.FirstName = estimateVM.FirstName;
            model.LastName = estimateVM.LastName;
            model.Company = estimateVM.Company;
            model.Email = estimateVM.Email;
            model.PhoneNo = estimateVM.PhoneNumber;
            model.Addressline1 = estimateVM.Addressline1;
            model.Addressline2 = estimateVM.Addressline2;
            model.City = estimateVM.City;
            model.State = estimateVM.State;
            model.ZipCode = estimateVM.ZipCode;
            model.CompanyID = estimateVM.CompanyID;
            model.EstimateID = estimateVM.EstimateID;
            model.CreatedBy = 1;
            model.Flag = "D";
            var response = await _estimatesRepository.DeleteEstimateAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
    }
}
