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

    }


}










      

