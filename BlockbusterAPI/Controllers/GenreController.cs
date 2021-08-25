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
                            SELECT Id, Title,
                            CONVERT(varchar(10),ReleaseDate,120) as ReleaseDate,
                            IsActive
                            FROM dbo.Movie
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
                            INSERT INTO dbo.Movie
                            (Title,ReleaseDate,IsActive,Genre)
                            VALUES
                            (
                            '" + genre.Title + @"'
                            , '" + genre.ReleaseDate + @"'
                            , '" + genre.IsActive + @"'
                            )";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
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
                            UPDATE dbo.Movie SET
                            Title = '" + genre.Title + @"'
                            , ReleaseDate = '" + genre.ReleaseDate + @"'
                            , IsActive = '" + genre.IsActive + @"'
                            WHERE Id = " + genre.Id + @"
                            ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
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
                            DELETE FROM dbo.Movie
                            WHERE Id = " + id + @"
                           ";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("BlockbusterAppCon");
            SqlDataReader dataReader;
            using (SqlConnection dbConnection = new SqlConnection(dataSource))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, dbConnection))
                {
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
