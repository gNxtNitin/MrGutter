using MrGutter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MrGutter.Repository
{
    public class APIWrapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string baseURL = "https://localhost:7014/api/";
        public APIWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
      
        public async Task<APIResponseModel> GetAsync(string APIName, string? JsonData)
        {
            APIResponseModel? resp = null;

            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];
                string url = baseURL + APIName;
                using HttpClient client = new();
                if (token != null)
                {
                 
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<APIResponseModel>(responseString);
            }
            catch (Exception ex)
            {
                resp = new APIResponseModel
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
            return resp;
        }
        public async Task<APIResponseModel> PostAsync(string APIName, string JsonData)
        {
            APIResponseModel? resp = null;
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];

                string url = baseURL + APIName;
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                HttpContent httpContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, httpContent);
                var responseString = await response.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<APIResponseModel>(responseString);
            }
            catch (Exception ex)
            {
                resp = new APIResponseModel
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
            return resp;
        }
    }
}
