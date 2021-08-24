using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, Title, Release, Active
                            from dbo.Movie
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
        
        public IActionResult Index()
        {
             return View();
        }
    }


}

