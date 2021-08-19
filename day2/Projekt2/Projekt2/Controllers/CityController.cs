using Project.Model;
using Project.Service;

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

        CityService cityService = new CityService();

        [HttpGet]
        [Route("city")]
        public HttpResponseMessage GetAllCity()
        {
            List<City> cities = cityService.GetAllCity();
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }


        [HttpGet]
        [Route("city/{id:int:min(1)}")]
        public HttpResponseMessage GetCityById([FromUri] int id)
        {
            City city = cityService.GetCityById(id);

            if (city.CityID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "City doesnt exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpGet]
        [Route("city/{name:alpha}")]
        public HttpResponseMessage GetCityByName(string name)
        {
            City city = cityService.GetCityByName(name);

            if (city.CityID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "City doesnt exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpPost]
        [Route("city")]
        public HttpResponseMessage PostNewCity([FromBody] City city)
        {

            cityService.CreateCity(city);
            return Request.CreateResponse(HttpStatusCode.OK, "" + city.Name + " is created");
        }


        [HttpPut]
        [Route("city/{id}")]
        public HttpResponseMessage UpdateCity(int id, [FromBody] City city)
        {
           

            bool result = cityService.UpdateCity(id, city);

            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "" + city.Name + " is updated");
        }


        [HttpDelete]
        [Route("city/{Id}")]
        public HttpResponseMessage DeleteCity(int id)
        {
            bool result = cityService.DeleteCity(id);
            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "City with id " + id + " is deleted");
        }
    }
}
