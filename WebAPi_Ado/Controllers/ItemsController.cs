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
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IConfiguration configuration, ILogger<ItemsController> logger)
        {
            _dataAccessItems = new DataAccessItems(configuration);
            _logger = logger;
        }

        // GET: api/items
        [HttpGet]
        public ActionResult<List<Item>> GetItems()
        {
            _logger.LogInformation("Getting items...");
            try {
                List<Item> items = _dataAccessItems.GetItems();
                _logger.LogInformation($"Retrieved {items.Count} items.");
                return Ok(items);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while getting items.");
                return StatusCode(500, "An error occurred while fetching items.");
            }
           
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            _logger.LogInformation("Getting items by ID");
            try
            {
                Item item = _dataAccessItems.GetItems(id);

                if (item == null)
                {
                    _logger.LogWarning($"Item with ID {id} not found.");
                    return NotFound();
                }

                _logger.LogInformation($"Retrieved item: {JsonConvert.SerializeObject(item)}");

                return Ok(item);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, $"Error while getting item with ID: {id}");
                return StatusCode(500, "An error occurred while fetching the item.");
            }
            
        }

        // POST: api/items
        [HttpPost]
        public  ActionResult PostItem(Item newItem)
        {
            _logger.LogInformation("Adding a new item.");
            try
            {
                int rowsAffected = _dataAccessItems.AddItem(newItem);

                if (rowsAffected > 0)
                {
                    _logger.LogInformation($"Added item: {JsonConvert.SerializeObject(newItem)}");
                    return Ok("Item added successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to add item.");
                    return BadRequest("Failed to add item.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a new item.");
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/items/{id}
        [HttpPut]
        public ActionResult Result(Item updatedItem)
        {
            _logger.LogInformation("Updating Items");
            try
            {
                int rowsAffected = _dataAccessItems.UpdateItems(updatedItem);

                if (rowsAffected > 0)
                {
                    _logger.LogInformation($"Item Updated {JsonConvert.SerializeObject(updatedItem)}");
                    return Ok("Item updated successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to Update Item.");
                    return NotFound("Item not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Updating a new Item.");
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public  ActionResult DeleteItem(int id)
        {
            _logger.LogInformation("Deleting Item...");
            int rowsAffected = _dataAccessItems.DeleteItem(id);
            try
            {
                

                if (rowsAffected > 0)
                {
                    _logger.LogInformation($"Item {id} Deleted Successfully");
                    return Ok("Item deleted successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to Delete Item");
                    return NotFound("Item not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Updating a new Item.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}



