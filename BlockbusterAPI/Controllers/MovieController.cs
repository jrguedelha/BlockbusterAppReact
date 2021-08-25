using BlockbusterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlockbusterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Solicitações GET
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT Id, Title,
                            CONVERT(varchar(10),ReleaseDate,120) as ReleaseDate,
                            IsActive,Genre
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
        public JsonResult Post(Movie movie)
        {
            string query = @"
                            INSERT INTO dbo.Movie
                            (Title,ReleaseDate,IsActive,Genre)
                            VALUES
                            (
                            '" + movie.Title + @"'
                            , '" + movie.ReleaseDate + @"'
                            , '" + movie.IsActive + @"'
                            , '" + movie.Genre + @"'
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
        public JsonResult Put(Movie movie)
        {
            string query = @"
                            UPDATE dbo.Movie SET
                            Title = '" + movie.Title + @"'
                            , ReleaseDate = '" + movie.ReleaseDate + @"'
                            , IsActive = '" + movie.IsActive + @"'
                            , Genre = '" + movie.Genre + @"'
                            WHERE Id = " + movie.Id + @"
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

