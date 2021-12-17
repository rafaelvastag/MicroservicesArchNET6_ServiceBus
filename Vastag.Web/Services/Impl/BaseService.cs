using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vastag.Web.Models;
using Vastag.Web.Models.DTOs;

namespace Vastag.Web.Services.Impl
{
    public class BaseService : IBaseService
    {
        public ResponseDTO response { get; set; }

        IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            response = new ResponseDTO();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest request)
        {
            try
            {
                var client = _httpClient.CreateClient("VastagAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                client.DefaultRequestHeaders.Clear();

                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                        encoding: Encoding.UTF8,
                        mediaType: "application/json");
                }

                HttpResponseMessage response = null;

                message.Method = request.ApiType switch
                {
                    Constants.SD.ApiType.GET => HttpMethod.Get,
                    Constants.SD.ApiType.POST => HttpMethod.Post,
                    Constants.SD.ApiType.PUT => HttpMethod.Put,
                    Constants.SD.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                response = await client.SendAsync(message);

                var apiContent = await response.Content.ReadAsStringAsync();
                var responseDTO = JsonConvert.DeserializeObject<T>(apiContent);

                return responseDTO;
            }
            catch (Exception e)
            {

                var error = new ResponseDTO
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                };

                var res = JsonConvert.SerializeObject(error);
                var responseDTO = JsonConvert.DeserializeObject<T>(res);
                return responseDTO;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
