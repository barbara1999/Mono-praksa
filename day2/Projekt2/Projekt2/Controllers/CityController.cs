using Projekt2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projekt2.Controllers
{
    [RoutePrefix("api")]


    public class CityController : ApiController
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString);

        [HttpGet]
        [Route("city")]
        public HttpResponseMessage GetAllCity()
        {

            SqlCommand command = new SqlCommand("SELECT * FROM City", connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<City> cities = new List<City>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    City city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    cities.Add(city);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }


        [HttpGet]
        [Route("city/{id:int:min(1)}")]
        public HttpResponseMessage GetCityById([FromUri] int id)
        {
            City city = new City();
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();



            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }

            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }

            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, city);

        }

        [HttpGet]
        [Route("city/{name:alpha}")]
        public HttpResponseMessage GetCityByName(string name)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE Name={name} ", connection);
            City city = new City();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }
            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpPost]
        [Route("city")]
        public HttpResponseMessage PostNewCity([FromBody] City city)
        {
            City mCity = new City();
            mCity.CityID = city.CityID;

            mCity.Name = city.Name;
            mCity.PostNumber = city.PostNumber;

            SqlCommand command = new SqlCommand($"INSERT INTO City (CityID, Name ,PostNumber) VALUES ({mCity.CityID},'{mCity.Name}',{mCity.PostNumber})", connection);

            connection.Open();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            command.ExecuteNonQuery();
            connection.Close();
            return response;
        }
    }
}
