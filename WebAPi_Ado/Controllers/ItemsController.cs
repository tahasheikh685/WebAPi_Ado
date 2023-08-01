using Microsoft.AspNetCore.Mvc;
using Rest.BasicAuth;
using WebAPi_Ado.DataAccessLayer;
using WebAPi_Ado.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPi_Ado.Controllers
{

    //[Route("api/[controller]")]
    [ApiController]
    //[BasicAuthenticationAttribute]
    public class ItemsController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly DataAccessItems _dataAccessItems;
        public ItemsController(IConfiguration configuration)
        {
            _config = configuration;
            _dataAccessItems = new DataAccessItems(_config);
        }
        
        //Get
        [Route("GetItems")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            List<Item> items = await _dataAccessItems.GetItems();

            return Ok(items);
        }

        //Get by ID
        //[Route("GetItemsbyID")]
        [HttpGet]
        public async Task<IActionResult> GetItemsid(int id)
        {
            List<Item> items = await _dataAccessItems.GetItems();

            List<Item> filteredItems = items.Where(item => item.Id == id).ToList();

            if (filteredItems.Count == 0)
            {
                return NotFound();
            }

            return Ok(filteredItems);
        }




        //Post
        [Route("PostItem")]
        [HttpPost]
        public async Task<IActionResult> AddItem(Item par)
        {
            try
            {
                int rowsAffected = await _dataAccessItems.AddItem(par);

                if (rowsAffected > 0)
                    return Ok(par);
                else
                    return BadRequest("Failed to add item.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Update
        [Route("UpdateItem")]
        [HttpPut]
        public async Task<IActionResult> UpdateItem(int id, Item updatedItem)
        {
            try
            {
                int rowsAffected = await _dataAccessItems.UpdateItems(id, updatedItem);

                if (rowsAffected > 0)
                    return Ok(updatedItem);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Delete
        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                int rowsAffected = await _dataAccessItems.DeleteItem(id);

                if (rowsAffected > 0)
                    return Ok($"Item with Id {id} has been deleted.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }





    }


}
