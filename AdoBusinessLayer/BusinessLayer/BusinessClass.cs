using AdoBusinessLayer.ClientLayer;
using AdoBusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdoBusinessLayer.BusinessLayer
{
    public class BusinessClass
    {
        private readonly Client _client;
        public BusinessClass()
        {
            _client = new Client();
        }

        public List<ItemsBL> GetALLRecords() 
        {
            return _client.GetALLRecords();
        }

        public ItemsBL GetByID(int Id)
        {
            return _client.GetByID(Id);
        }

        public IActionResult Create(ItemsBL item)
        {
            return _client.Create(item);
        }
        public IActionResult Edit(ItemsBL item) 
        {
            return _client.Edit(item);
        }
        public IActionResult Delete(int id)
        {
            return _client.Delete(id);
        }

    }
}
