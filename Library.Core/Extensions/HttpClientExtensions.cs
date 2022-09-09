using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Newtonsoft.Json;
using System.Text;

namespace Library.Core.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }

        public static async Task<TResponse> PutJsonAsync<TResponse, TRequest>(this HttpClient httpClient, string url, TRequest entity)
        {
            var uri = new Uri(url);
            var stringEntity = JsonConvert.SerializeObject(entity);
            var content = new StringContent(stringEntity, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = uri,
                Content = content
            };
            var response = await httpClient.SendAsync(request);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(resultString);
            return result;
        }
        public static async Task<TResponse> PostJsonAsync<TResponse, TRequest>(this HttpClient httpClient, string url, TRequest entity)
        {
            var uri = new Uri(url);
            var stringEntity = JsonConvert.SerializeObject(entity);
            var content = new StringContent(stringEntity, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };
            var response = await httpClient.SendAsync(request);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(resultString);
            return result;
        }
        public static async Task<T> DeleteJsonAsync<T>(this HttpClient client, string url)
        {
            var response = await client.DeleteAsync(url);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }
    }
}
