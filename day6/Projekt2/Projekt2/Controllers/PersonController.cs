using Project.Model;
using Project.Model.Common;
using Project.Service;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace Projekt2.Controllers
{
    

    [RoutePrefix("api")]
    public class PersonController : ApiController
    {
        IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("person")]
        public async Task<HttpResponseMessage> GetAllPersonAsync()
        {
            List<IPerson> peoples = await _personService.GetAllPeopleAsync();

            return Request.CreateResponse(HttpStatusCode.OK, peoples);
        }

        [HttpGet]
        [Route("person/{id:int:min(1)}")]
        public async Task<HttpResponseMessage> GetPersonByIdAsync([FromUri]int id)
        {
            IPerson person =await _personService.GetPersonByIdAsync(id);

            if (person.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Person doesn't exist");
            }

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
        
        [HttpGet]
        [Route("person/{name:alpha}")]
        public async Task<HttpResponseMessage> GetPersonByNameAsync([FromUri] string name)
        {
            List <IPerson> people = await _personService.GetPersonByNameAsync(name);

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }

        [HttpPost]
        [Route("person")]
        public async Task<HttpResponseMessage> PostNewPerson([FromBody] IPerson person)
        {
           bool result= await _personService.CreatePersonAsync(person);

            if (person.Id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Id must be bigger than 0");
            }
            else if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Person with that ID already exist");
            }
           
            return Request.CreateResponse(HttpStatusCode.OK, "" + person.Name + " is created");
        }

        [HttpPut]
        [Route("person/{id}")]
        public async Task<HttpResponseMessage> UpdatePersonAsync(int id, [FromBody] Person person)
        {
            /* rjesiti kad budes znala
             SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Person",connection);

             dataAdapter.UpdateCommand = new SqlCommand($"UPDATE Person SET City={cityId} WHERE ID={id}", connection);

             dataAdapter.UpdateCommand.Parameters.Add("@cityID", SqlDbType.Int, 10, "City");
             dataAdapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 10, "ID");

             DataTable personTabel = new DataTable();
             dataAdapter.Fill(personTabel);
             dataAdapter.Update(personTabel);

             DataRow row = personTabel.Rows[0];

             Person person = new Person(id,row[1].ToString(),row[2].ToString(),cityId);

             return Request.CreateResponse(HttpStatusCode.OK, person);

             */

            bool result=await _personService.UpdatePersonAsync(id, person);

            if (result!=true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }
           
            return Request.CreateResponse(HttpStatusCode.OK,""+person.Name+" is updated");

        }


        [HttpDelete]
        [Route("person/{Id}")]
        public async Task<HttpResponseMessage> DeletePersonAsync(int id)
        {
            bool result = await _personService.DeletePersonAsync(id);
            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Person with id "+id+" is deleted");
        }

    }
}
