using Project.Model;
using Project.Service;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Projekt2.Controllers
{
    [RoutePrefix("api")]


    public class CityController : ApiController
    {
        CityService cityService = new CityService();

        [HttpGet]
        [Route("city")]
        public async Task<HttpResponseMessage> GetAllCityAsync()
        {
            List<City> cities =await  cityService.GetAllCityAsync();
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        [HttpGet]
        [Route("city/{id:int:min(1)}")]
        public async Task<HttpResponseMessage> GetCityByIdAsync([FromUri] int id)
        {
            City city = await cityService.GetCityByIdAsync(id);

            if (city.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "City doesnt exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpGet]
        [Route("city/{name:alpha}")]
        public async Task<HttpResponseMessage> GetCityByNameAsync(string name)
        {
            City city = await cityService.GetCityByNameAsync(name);

            if (city.CityID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "City doesnt exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpPost]
        [Route("city")]
        public async Task<HttpResponseMessage> PostNewCity([FromBody] City city)
        { 
            bool result =await cityService.CreateCityAsync(city);

            if (city.CityID <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Id must be bigger than 0");
            }
            else if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "City with that ID already exist");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "" + city.Name + " is created");
        }

        [HttpPut]
        [Route("city/{id}")]
        public async Task<HttpResponseMessage> UpdateCityAsync(int id, [FromBody] City city)
        {
            bool result = await cityService.UpdateCityAsync(id, city);

            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "" + city.Name + " is updated");
        }

        [HttpDelete]
        [Route("city/{Id}")]
        public async Task<HttpResponseMessage> DeleteCityAsync(int id)
        {
            bool result = await cityService.DeleteCityAsync(id);
            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "City with id " + id + " is deleted");
        }
    }
}
