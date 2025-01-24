using Microsoft.AspNetCore.Http;
using MrGutter.Models;
using MrGutter.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MrGutter.Repository
{
    public class EstimatesRepository : IEstimatesRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly APIWrapper _aPIWrapper;
        public EstimatesRepository(IHttpContextAccessor httpContextAccessor, APIWrapper aPIWrapper)
        {
            _aPIWrapper = aPIWrapper;
            _httpContextAccessor = httpContextAccessor;
        }
        EncryptDecrypt _encryptDecrypt = new EncryptDecrypt();

        public async Task<APIResponseModel> CreateEstimateAsync(EstimateModel estimateModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(estimateModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.PostAsync("Estimate/CreateOrSetEstimate", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> UpdateEstimate(EstimateModel estimateModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(estimateModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.PostAsync("Estimate/CreateOrSetEstimate", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> GetEstimatelistAsync(EstimateIdsModel estimateIdsModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string queryParameters = $"flag={estimateIdsModel.Flag}&userId={estimateIdsModel.UserId}&companyId={estimateIdsModel.CompanyID}&estimateId={estimateIdsModel.EstimateID}";
                response = await _aPIWrapper.GetAsync($"Estimate/GetEstimate?{queryParameters}", "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
            return response;
        }

        public async Task<APIResponseModel> GetStatuslistAsync(string? StatusId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string V = await _encryptDecrypt.Encrypt(StatusId.ToString());
                string reqStr = HttpUtility.UrlEncode(V);
                response = await _aPIWrapper.GetAsync("Estimate/GetStatus?statusId=", reqStr);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> ChangeEstimateStatus(EstimateModel estimateModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(estimateModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.PostAsync("Estimate/CreateOrSetEstimate", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> DeleteEstimateAsync(EstimateModel estimateModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(estimateModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.PostAsync("Estimate/CreateOrSetEstimate", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }


        public async Task<APIResponseModel> GetMeasurementCatListAsync(int CatId, int CompanyId)
        {
            APIResponseModel response = new APIResponseModel();
           
            try
            {
               
                response = await _aPIWrapper.GetAsync("Estimate/GetMeasurementCat?mCatId="+ CatId + "&companyId="+ CompanyId, "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> GetMeasurementTokenListAsync(int estimateId, int companyId, int mTokenId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {

                response = await _aPIWrapper.GetAsync("Estimate/GetMeasurementToken?estimateId=" + estimateId + "&companyId=" + companyId + "&mTokenId="+mTokenId,"");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> GetMeasurementUnitListAsync(int uMId, int companyId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {

                response = await _aPIWrapper.GetAsync("Estimate/GetMeasurementUnit?uMId=" + uMId + "&companyId=" + companyId, "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> CreateOrSetMeasurementTokenAsync(MeasurementTokenModel measurementToken)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(measurementToken);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);
                response = await _aPIWrapper.PostAsync("Estimate/CreateOrSetMeasurementToken", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
    }
}
