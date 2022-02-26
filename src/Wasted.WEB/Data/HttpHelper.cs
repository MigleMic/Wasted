using System;
using Serilog;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Wasted.WEB.Wrapped;
namespace Wasted.Data
{
    
    public class HttpHelper
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string ApiUrl = "https://wastedapi.azurewebsites.net/api/";

        public async Task<int> Post<T>(T data, string endpoint)
        {
            try
            {
                var response = await client.PostAsJsonAsync(ApiUrl+endpoint, data);
                if(response.StatusCode == HttpStatusCode.Created)
                {
                    return Int32.Parse(
                        response.Headers.Location.AbsolutePath.Substring(
                            response.Headers.Location.AbsolutePath.LastIndexOf('/') + 1
                        ));
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(int);
            }
           
        }
        public async void Put<T>(T data, string endpoint)
        {
            try
            {
                var response = await client.PutAsJsonAsync(ApiUrl+endpoint, data);
                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    return;
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
            }
           
        }
        public async void Delete(int id, string endpoint)
        {
            try
            {
                var response = await client.DeleteAsync(ApiUrl+endpoint+"/"+id);
                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    return;
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
            }
        }
        public async Task<List<T>> GetProductList<T>(string endpoint)
        {
            try
            {
                return JsonConvert.DeserializeObject<PagedResponse<List<T>>>(
                       await client.GetStringAsync(ApiUrl+endpoint)
                ).Data;
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(List<T>);
            } 
        }
        public async Task<PagedResponse<List<Product>>> GetPageResponse(string endpoint)
        {
            try
            {
                return JsonConvert.DeserializeObject<PagedResponse<List<Product>>>(
                       await client.GetStringAsync(ApiUrl+endpoint)
                );
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(PagedResponse<List<Product>>);
            } 
        }
        public async Task<List<T>> GetList<T>(string endpoint)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(
                       await client.GetStringAsync(ApiUrl+endpoint)
                );
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(List<T>);
            }
           
        }
        public async Task<T[]> GetArray<T>(string endpoint)
        {
            try
            {
                return JsonConvert.DeserializeObject<T[]>(
                       await client.GetStringAsync(ApiUrl+endpoint)
                );
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(T[]);
            }
           
        }
        public async Task<T> GetById<T>(int id, string endpoint)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(
                    await client.GetStringAsync(ApiUrl+endpoint+"/"+id)
                );
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
                return default(T);
            }
           
        }

    }

}
