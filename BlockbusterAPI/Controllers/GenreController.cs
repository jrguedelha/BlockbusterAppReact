using BlockbusterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlockbusterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : Controller
    {
        private readonly IConfiguration _configuration;

        public GenreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT Id, Title, ReleaseDate, IsActive
                            FROM dbo.Genre
                            ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    dataReader = sqlCommand.ExecuteReader();
                    table.Load(dataReader);
                    dataReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Genre genre)
        {
            string query = @"
                            INSERT INTO dbo.Genre
                            (Title)
                            VALUES (@Title)
                            ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Title", genre.Title);
                    dataReader = sqlCommand.ExecuteReader();
                    table.Load(dataReader);
                    dataReader.Close();
                    dbConnection.Close();
                }
            }

            return new JsonResult("Added Successfuly");
        }

        [HttpPut]
        public JsonResult Put(Genre genre)
        {
            string query = @"
                            UPDATE dbo.Genre
                            SET Title = @Title
                            WHERE Id = @Id
                            ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", genre.Id);
                    sqlCommand.Parameters.AddWithValue("@Title", genre.Title);
                    dataReader = sqlCommand.ExecuteReader();
                    table.Load(dataReader);
                    dataReader.Close();
                    dbConnection.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            DELETE FROM dbo.Genre
                            WHERE Id = @Id
                            ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    dataReader = sqlCommand.ExecuteReader();
                    table.Load(dataReader);
                    dataReader.Close();
                    dbConnection.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
