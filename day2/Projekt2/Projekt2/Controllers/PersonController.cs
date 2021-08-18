using Projekt2.Models;
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

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString);


        [HttpGet]
        [Route("person")]
        public HttpResponseMessage GetAllPerson()
        {
            
            SqlCommand command = new SqlCommand("SELECT * FROM Person",connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Person> people = new List<Person>();

            if (reader.HasRows)
            {
                while (reader.Read())
                { 
                    Person person =  new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, people);  
        }



        [HttpGet]
        [Route("person/{id:int:min(1)}")]
        public HttpResponseMessage GetPersonById([FromUri]int id)
        {
            Person person = new Person();
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    
                }
                
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }

            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, person);
            
            

        }
        
        [HttpGet]
        [Route("person/{name:alpha}")]
        public HttpResponseMessage GetPersonByName(string name)
        {
            Person person = new Person();
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE Name='{name}' ", connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                }
            }

            else
            {
                
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
                
            }
            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [HttpPost]
        [Route("person")]
        public HttpResponseMessage PostNewPerson([FromBody] Person person)
        {
            Person mPerson = new Person();
            mPerson.Id = person.Id;

            mPerson.Name = person.Name;
            mPerson.Surname = person.Surname;
            mPerson.City = person.City;
            
            SqlCommand command = new SqlCommand($"INSERT INTO Person (ID, Name , Surname , City ) VALUES ({mPerson.Id},'{mPerson.Name}','{mPerson.Surname}',{mPerson.City})", connection);
            
            connection.Open();

           
            var response = Request.CreateResponse(HttpStatusCode.Created);
            command.ExecuteNonQuery();
            connection.Close();
            return response;
        }

        [HttpPut]
        [Route("person/{Id}")]
        public HttpResponseMessage UpdatePersonAddress(int id, [FromBody] int city)
        {

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT City FROM Person", connection);

            dataAdapter.UpdateCommand = new SqlCommand($"UPDATE Person SET City={city} WHERE ID={id}", connection);

            dataAdapter.UpdateCommand.Parameters.Add("@city", SqlDbType.Int, 10, "City");





            /*
            SqlCommand command = new SqlCommand($"UPDATE Person SET City={city} WHERE ID={id}");
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            */
            var response = new HttpResponseMessage();
            response.Headers.Add("Message", "Address is successfuly updated");
            return response;
            
           
        }

        [HttpDelete]
        [Route("person/{Id}")]
        public HttpResponseMessage Delete(int id)
        {
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.DeleteCommand = new SqlCommand($"DELETE FROM Person WHERE ID={id}; ", connection);

            adapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");

            adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, "Student deleted");
            
            connection.Close();
            return Msg;

        }
       

        
    }
}
