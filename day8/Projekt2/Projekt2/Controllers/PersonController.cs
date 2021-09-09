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
using Project.Common;



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
        public async Task<HttpResponseMessage> GetAllPersonAsync([FromUri]Sorter sorter, [FromUri]Pager pager, [FromUri]PersonFilter filter)
        {

            List<PersonRest> people = (await _personService.GetAllPeopleAsync(sorter,pager,filter)).ConvertAll(ConverFromDomainToRest);

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }

        [HttpGet]
        [Route("person/{id:int:min(1)}")]
        public async Task<HttpResponseMessage> GetPersonByIdAsync([FromUri]int id)
        {
            PersonRest person =ConverFromDomainToRest(await _personService.GetPersonByIdAsync(id));

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
            List <PersonRest> people = (await _personService.GetPersonByNameAsync(name)).ConvertAll(ConverFromDomainToRest);

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }

        [HttpPost]
        [Route("person")]
        public async Task<HttpResponseMessage> PostNewPerson([FromBody] PersonRest person)
        {
            Person personDomain = ConvertFromRestToDomain(person);
           bool result= await _personService.CreatePersonAsync(personDomain);

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
        public async Task<HttpResponseMessage> UpdatePersonAsync(int id, [FromBody] PersonRest person)
        {
            Person personDomain = ConvertFromRestToDomain(person);
            bool result=await _personService.UpdatePersonAsync(id, personDomain);

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

        public class PersonRest
        {
            public PersonRest()
            {
            }

            public PersonRest(int id, string name, string surname, int city)
            {
                Id = id;
                Name = name;
                Surname = surname;
                City = city;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int City { get; set; }
        }

        public PersonRest ConverFromDomainToRest(IPerson domainPerson)
        {
            PersonRest personRest = new PersonRest(domainPerson.Id, domainPerson.Name, domainPerson.Surname, domainPerson.City);
            return personRest;
        }

        public Person ConvertFromRestToDomain(PersonRest restPerson)
        {
            Person person = new Person(restPerson.Id, restPerson.Name, restPerson.Surname, restPerson.City);
            return person;
        }
    }
}
