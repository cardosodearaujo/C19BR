using Newtonsoft.Json;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Security;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Exceptions;
using System.Net;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.HttpTransaction
{
    public class HttpRequest
    {
        private const string MediaType = "application/json";
        private const double TimeOut = 30; //Minutos
        public SecurityToken Token;
        public bool ValidToken;

        public HttpRequest()
        {

        }

        public HttpRequest(SecurityToken Token, bool ValidToken)
        {
            this.Token = Token;
            this.ValidToken = ValidToken;
        }

        private HttpClient GetService()
        {
            if (ValidToken)
            {
                if (Token == null || string.IsNullOrEmpty(Token.Token)) throw new InternalApiException("Token inexistente!");
                var service = new HttpClient();
                service.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Token);
                return service;
            }
            else
            {
                return new HttpClient();
            }           
        }
        public async Task<HttpRequestResponse> Get(string URL)
        {
            try
            {
                var service = GetService();
                service.Timeout = TimeSpan.FromMinutes(TimeOut);
                var result = await service.GetAsync(URL);

                if (result.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse
                    {
                        Success = result.IsSuccessStatusCode,
                        HttpResult = result
                    };
                }
                else
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return new HttpRequestResponse
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
        public async Task<HttpRequestResponse> Post(string URL, object sender)
        {
            try
            {
                var service = GetService();
                service.Timeout = TimeSpan.FromMinutes(TimeOut);
                var objectSerealized = JsonConvert.SerializeObject(sender);
                var content = new StringContent(objectSerealized, Encoding.UTF8, MediaType);
                var result = await service.PostAsync(URL, content);

                if (result.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse
                    {
                        Success = result.IsSuccessStatusCode,
                        HttpResult = result
                    };
                }
                else
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return new HttpRequestResponse
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
        public async Task<HttpRequestResponse> Put(string URL, object sender)
        {
            try
            {
                var service = GetService();
                service.Timeout = TimeSpan.FromMinutes(TimeOut);
                var objectSerealized = JsonConvert.SerializeObject(sender);
                var content = new StringContent(objectSerealized, Encoding.UTF8, MediaType);
                var result = await service.PutAsync(URL, content);

                if (result.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse
                    {
                        Success = result.IsSuccessStatusCode,
                        HttpResult = result
                    };
                }
                else
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return new HttpRequestResponse
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
        public async Task<HttpRequestResponse> Delete(string URL)
        {
            try
            {
                var service = GetService();
                service.Timeout = TimeSpan.FromMinutes(TimeOut);
                var result = await service.DeleteAsync(URL);

                if (result.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse
                    {
                        Success = result.IsSuccessStatusCode,
                        HttpResult = result
                    };
                }
                else
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return new HttpRequestResponse
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
        public T Download<T>(string URL) 
        {
            var valor = new WebClient().DownloadString(URL);
            return JsonConvert.DeserializeObject<T>(valor);
        }
    }
}
