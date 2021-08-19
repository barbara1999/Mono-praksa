using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Projekt2.Controllers
{
    

    [RoutePrefix("api")]
    public class PersonController : ApiController
    {
        PersonService personService = new PersonService();
        [HttpGet]
        [Route("person")]
        public HttpResponseMessage GetAllPerson()
        {
            List<Person> people = personService.GetAllPeople();

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }

        [HttpGet]
        [Route("person/{id:int:min(1)}")]
        public HttpResponseMessage GetPersonById([FromUri]int id)
        {
            Person person = personService.GetPersonById(id);

            if (person.Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Person doesnt exist");
            }

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
        
        [HttpGet]
        [Route("person/{name:alpha}")]
        public HttpResponseMessage GetPersonByName([FromUri] string name)
        {
            List <Person> people = personService.GetPersonByName(name);

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }

        [HttpPost]
        [Route("person")]
        public HttpResponseMessage PostNewPerson([FromBody] Person person)
        {
            personService.CreatePerson(person);
            return Request.CreateResponse(HttpStatusCode.OK, "" + person.Name + " is created");
        }

        [HttpPut]
        [Route("person/{id}")]
        public HttpResponseMessage UpdatePerson(int id, [FromBody] Person person)
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

            bool result=personService.UpdatePerson(id, person);

            if (result!=true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }
           
            return Request.CreateResponse(HttpStatusCode.OK,""+person.Name+" is updated");

        }


        [HttpDelete]
        [Route("person/{Id}")]
        public HttpResponseMessage DeletePerson(int id)
        {
            bool result = personService.DeletePerson(id);
            if (result != true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Person with id "+id+" is deleted");
        }

    }
}
