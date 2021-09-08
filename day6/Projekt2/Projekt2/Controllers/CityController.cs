using Project.Model;
using Project.Model.Common;
using Project.Service;
using Project.Service.Common;
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

        ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("city")]
        public async Task<HttpResponseMessage> GetAllCityAsync()
        {
            List<CityRest> cities = (await _cityService.GetAllCityAsync()).ConvertAll(ConvertFromDomainToRest);
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        [HttpGet]
        [Route("city/{id:int:min(1)}")]
        public async Task<HttpResponseMessage> GetCityByIdAsync([FromUri] int id)
        {
            CityRest city = ConvertFromDomainToRest(await _cityService.GetCityByIdAsync(id));

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
            CityRest city = ConvertFromDomainToRest(await _cityService.GetCityByNameAsync(name));

            if (city.CityID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "City doesnt exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        [HttpPost]
        [Route("city")]
        public async Task<HttpResponseMessage> PostNewCity([FromBody] CityRest city)
        {
            ICity cityDomain = ConvertFromRestToDomain(city);

            bool result = await _cityService.CreateCityAsync(cityDomain);

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
        public async Task<HttpResponseMessage> UpdateCityAsync(int id, [FromBody] CityRest city)
        {
            ICity cityDomain = ConvertFromRestToDomain(city);
            bool result = await _cityService.UpdateCityAsync(id, cityDomain);

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
            bool result = await _cityService.DeleteCityAsync(id);
            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "City does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "City with id " + id + " is deleted");
        }
        public class CityRest
        {
            public int CityID { get; set; }
            public string Name { get; set; }
            public int PostNumber { get; set; }

            public CityRest() { }

            public CityRest(int cityID, string name, int postNumber)
            {
                CityID = cityID;
                Name = name;
                PostNumber = postNumber;
            }
        }

        public CityRest ConvertFromDomainToRest(ICity domainCity)
        {
            CityRest cityRest = new CityRest(domainCity.CityID,domainCity.Name,domainCity.PostNumber);
            
            return cityRest;
        }

        public City ConvertFromRestToDomain(CityRest restCity)
        {
            City cityDomain = new City(restCity.CityID, restCity.Name, restCity.PostNumber);
         
            return cityDomain;
        }
    }
}
