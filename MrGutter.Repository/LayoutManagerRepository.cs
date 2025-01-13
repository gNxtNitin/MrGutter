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
    public class LayoutManagerRepository: ILayoutManagerRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly APIWrapper _aPIWrapper;
        public LayoutManagerRepository(IHttpContextAccessor httpContextAccessor, APIWrapper aPIWrapper)
        {
            _aPIWrapper = aPIWrapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<APIResponseModel> CreateLayoutAsync(CreateReportLayoutModel createReportLayoutModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(createReportLayoutModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the APIpl
                response = await _aPIWrapper.PostAsync("Account/AuthenticateUser", json);
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
