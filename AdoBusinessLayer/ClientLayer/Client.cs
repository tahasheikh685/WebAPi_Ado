using AdoBusinessLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdoBusinessLayer.ClientLayer
{
    public class Client
    {
        private readonly Uri _baseAddress;
        private readonly HttpClient _httpClient;
        public Client(IConfiguration configuration)
        {
            _baseAddress = new Uri(configuration["URLs:APIURL"]);
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        public List<ItemsBL> GetALLRecords()
        {
            List<ItemsBL> itemList = new List<ItemsBL>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemList = JsonConvert.DeserializeObject<List<ItemsBL>>(data);
            }
            return itemList;
        }

        public IActionResult Create(ItemsBL item)
        {
            try
            {
                string data = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

        public ItemsBL GetByID(int Id)
        {

            ItemsBL item = new ItemsBL();
            HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}{Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<ItemsBL>(data);
            }

            return item;
        }

        public IActionResult Edit(ItemsBL item)
        {
            try
            {
                string data = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {

                return new BadRequestResult();
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync($"{_httpClient.BaseAddress}{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            return new BadRequestResult();
        }
    }
}
