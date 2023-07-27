using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Linq;
using WebAPi_Ado.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPi_Ado.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IConfiguration _config;
        public ItemsController(IConfiguration configuration)
        {
            _config = configuration;
        }
        
        //Get
        [Route("GetItem")]
        [HttpGet]

        public async Task<IActionResult> GetItem()
        {
            List<Item> items = new List<Item>();
            
            using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                string query = "SELECT * FROM Items";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();



                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = Convert.ToInt32(reader["Id"]);
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Price = Convert.ToDecimal(reader["Price"]);
                    items.Add(item);
                }

                conn.Close();
            }

            return Ok(items);
        }

        

        //Post
        [Route("PostItem")]
        [HttpPost]
        public async Task<IActionResult> PostItem(Item par)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    string query = "INSERT INTO Items (Name, Description, Price) VALUES (@Name, @Description, @Price)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Name", par.Name);
                    cmd.Parameters.AddWithValue("@Description", par.Description);
                    cmd.Parameters.AddWithValue("@Price", par.Price);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                }

                return Ok(par);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update
        [Route("UpdateItem")]
        [HttpPut]
        public async Task<IActionResult> UpdateItem(int id, Item updatedItem)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    string query = "UPDATE Items SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", updatedItem.Name);
                    cmd.Parameters.AddWithValue("@Description", updatedItem.Description);
                    cmd.Parameters.AddWithValue("@Price", updatedItem.Price);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(updatedItem);
                    }
                    else
                    {
                        return NotFound(); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete
        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    string query = "DELETE FROM Items WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Item with Id {id} has been deleted.");
                    }
                    else
                    {
                        return NotFound(); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }


}
