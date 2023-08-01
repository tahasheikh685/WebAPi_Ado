using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebAPi_Ado.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPi_Ado.DataAccessLayer
{
    public class DataAccessItems
    {
        private readonly IConfiguration _config;
        public DataAccessItems(IConfiguration configuration)
        {
            _config = configuration;
        }
        //Get All Items
        public async Task<List<Item>> GetItems()
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
                    Item item = new Item
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                    items.Add(item);
                }
            conn.Close();
            }

            return items;
        }

        //Get Items by Id
        
        public async Task<Item> GetItems(int id)
        {
            Item item = null;

            using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT * FROM Items WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = new Item
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                }

                conn.Close();
            }

            return item;
        }



        // Add Items
        public async Task<int> AddItem(Item par)
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
                    int rowsaffected= await cmd.ExecuteNonQueryAsync();
                    return rowsaffected;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update Item
        
        public async Task<int> UpdateItems(int id, Item updatedItem)
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
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Items
        
        public async Task<int> DeleteItem(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    string query = "DELETE FROM Items WHERE Id = @Id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
