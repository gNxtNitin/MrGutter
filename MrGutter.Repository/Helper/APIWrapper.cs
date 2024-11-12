using MrGutter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace MrGutter.Repository
{
    public class APIWrapper
    {
        EncryptDecrypt encryptDecrypt = new EncryptDecrypt();   
        // string baseURL= DNFINSResources.GetAPIBaseURL();
        //string baseURL= "https://localhost:7014/api/";
        string baseURL= "https://localhost:7014/api/";
        
        //Method to call the GET API
        public async Task<APIResponseModel> GetAsync(string APIName,string? JsonData, string? token)
        {
            APIResponseModel? resp = null;
            try
            {
                
               // JsonData = HttpUtility.UrlEncode(JsonData);
                string url = baseURL + APIName + JsonData;
                using HttpClient client = new();
                //client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                         
                HttpResponseMessage response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<APIResponseModel>(responseString);
            }
            catch (Exception ex)
            {
                resp = new APIResponseModel();
                resp.Code = -1;
                resp.Msg = ex.Message;
            }
            return resp;
        }

        //Method to call the POST API
        public async Task<APIResponseModel> PostAsync(string APIName, string JsonData, string? token)
        {
            APIResponseModel? resp = null;
            string url = baseURL + APIName;
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", token);
            try
            {
                HttpContent httpContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, httpContent);
                var responseString = await response.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<APIResponseModel>(responseString);
            }
            catch (Exception ex)
            {
                resp = new APIResponseModel();
                resp.Code = -1;
            }
            return resp;
        }
    }
}
