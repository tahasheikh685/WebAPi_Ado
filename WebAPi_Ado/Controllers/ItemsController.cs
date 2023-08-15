using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rest.BasicAuth;
using WebAPi_Ado.DataAccessLayer;
using WebAPi_Ado.Models;

namespace WebAPi_Ado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataAccessItems _dataAccessItems;

        public ItemsController(IConfiguration configuration)
        {
            _dataAccessItems = new DataAccessItems(configuration);
        }

        // GET: api/items
        [HttpGet]
        public ActionResult<List<Item>> GetItems()
        {
            List<Item> items = _dataAccessItems.GetItems();
            return Ok(items);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            Item item = _dataAccessItems.GetItems(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public  ActionResult item(Item newItem)
        {
            try
            {
                int rowsAffected = _dataAccessItems.AddItem(newItem);

                if (rowsAffected > 0)
                {
                    return Ok("Item added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add item.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/items/{id}
        [HttpPut]
        public ActionResult Result(Item updatedItem)
        {
            try
            {
                int rowsAffected = _dataAccessItems.UpdateItems(updatedItem);

                if (rowsAffected > 0)
                {
                    return Ok("Item updated successfully.");
                }
                else
                {
                    return NotFound("Item not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public  ActionResult DeleteItem(int id)
        {
            try
            {
                int rowsAffected = _dataAccessItems.DeleteItem(id);

                if (rowsAffected > 0)
                {
                    return Ok("Item deleted successfully.");
                }
                else
                {
                    return NotFound("Item not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}



