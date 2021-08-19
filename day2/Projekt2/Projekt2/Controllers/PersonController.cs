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
        [Route("person/{id}")]
        public HttpResponseMessage UpdatePersonAddress(int id, [FromBody] Person person)
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

     
            Person mPerson = new Person();
            mPerson.Id = person.Id;
            mPerson.Name = person.Name;
            mPerson.Surname = person.Surname;
            mPerson.City = person.City;

          
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);
            connection.Open();

            SqlDataReader reader = commandForID.ExecuteReader();
            

            if (!reader.HasRows)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");
            }

            reader.Close();

            SqlCommand command = new SqlCommand($"UPDATE Person SET City={mPerson.City}, Name='{mPerson.Name}', Surname='{mPerson.Surname}' WHERE ID={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK);

        }


        [HttpDelete]
        [Route("person/{Id}")]
        public HttpResponseMessage Delete(int id)
        {
          

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);
            SqlCommand command = new SqlCommand();
            connection.Open();

            SqlDataReader reader = commandForID.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    command = new SqlCommand($"DELETE FROM Person WHERE ID={id}; ", connection);
                }
            }

            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist");

            }

            reader.Close();
           
            command.ExecuteNonQuery();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
       

        
    }
}
