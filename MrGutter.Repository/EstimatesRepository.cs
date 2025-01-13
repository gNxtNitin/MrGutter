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
        public async Task<APIResponseModel> GetEstimatelistAsync(string? EstimateId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string V = await _encryptDecrypt.Encrypt(EstimateId.ToString());
                string reqStr = HttpUtility.UrlEncode(V);
                response = await _aPIWrapper.GetAsync("Estimate/GetEstimate?encReq=", reqStr);
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
