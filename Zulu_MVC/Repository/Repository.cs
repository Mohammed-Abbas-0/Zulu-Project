using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zulu_MVC.Repository.IRepository;

namespace Zulu_MVC.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (objToCreate is not null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public Task<bool> DeleteAync(string url, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string url, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAync(string url, T objToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
