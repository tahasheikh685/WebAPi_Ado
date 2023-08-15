using AdoBusinessLayer.BusinessLayer;
using AdoBusinessLayer.ClientLayer;
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
        private readonly BusinessClass _business;
        public ItemController()
        {
            _business = new BusinessClass();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ItemsBL> list = _business.GetALLRecords();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemsBL item)
        {
            // ItemsBL items = new ItemsBL();
            _business.Create(item);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Edit(int id) 
        {
            ItemsBL item= _business.GetByID(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(ItemsBL item) 
        {
            _business.Edit(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            ItemsBL item = _business.GetByID(id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) 
        {
            _business.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ItemsBL item = _business.GetByID(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult DeleteSelected(int[] ids)
        {
            try
            {
                // Delete the selected items by their IDs
                // Here, you can use your business layer or data access layer to perform the deletion.
                // For demonstration purposes, I'll just show a simple example of deleting the items.

                // In your actual implementation, replace this with the logic to delete the items.
                // For example, you can use your business layer's DeleteItem method to delete each item.
                foreach (var id in ids)
                {
                    // Call your business layer's DeleteItem method here
                    // Example: _itemsBusinessLayer.DeleteItem(id);
                }

                return Ok(); // Return a success response to the client
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during deletion
                return BadRequest(ex.Message); // Return a bad request response to the client with the error message
            }
        }
    }


}










      

