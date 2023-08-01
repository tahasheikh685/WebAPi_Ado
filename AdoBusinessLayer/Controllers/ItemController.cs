//using AdoBusinessLayer.Models;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace AdoBusinessLayer.Controllers
//{
//    public class ItemController : Controller
//    {
//        Uri baseAddress = new Uri("https://localhost:7180");
//        private readonly HttpClient _httpClient;
//        public ItemController()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = baseAddress;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            List<ItemsBL> itemList = new List<ItemsBL>();
//            HttpResponseMessage response= _httpClient.GetAsync(_httpClient.BaseAddress + "/GetItems").Result;
//            if (response.IsSuccessStatusCode) 
//            {
//                string data= response.Content.ReadAsStringAsync().Result;
//                itemList = JsonConvert.DeserializeObject<List<ItemsBL>>(data);
//            }
//            return View(itemList);
//        }
//    }
//}

using AdoBusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AdoBusinessLayer.Controllers
{
    public class ItemController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7180");
        private readonly HttpClient _httpClient;
        public ItemController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ItemsBL> itemList = new List<ItemsBL>();
            HttpResponseMessage response = _httpClient.GetAsync("/GetItems").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemList = JsonConvert.DeserializeObject<List<ItemsBL>>(data);
            }
            return View(itemList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemsBL item)
        {
            try 
            {
                string data = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("/PostItem", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Item Added.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int Id) 
        {
            try
            {
                ItemsBL item = new ItemsBL();
                HttpResponseMessage response = _httpClient.GetAsync("/GetItemsid?id=" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<ItemsBL>(data);
                }
                return View(item);
            }
            
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Edit(ItemsBL item)
        {
            try
            {
                string data = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("/UpdateItem", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Item Details Updated.";
                    return RedirectToAction("Index");
                }
                return View(item);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }
    }
}

