using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Project.Model;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class PersonRepository

    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString);

        public async Task<List<Person>> GetAllPersonAsync()
        {

            SqlCommand command = new SqlCommand("SELECT * FROM Person", connection);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<Person> people =   new List<Person>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Person person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }
            reader.Close();
            connection.Close();
            return people;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            
            Person person = new Person();
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                }
            }   
           
            reader.Close();
            connection.Close();
            return person;
        }

        public async Task<List<Person>> GetPersonByNameAsync(string name)
        {
            List<Person> people = new List<Person>();
           
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE Name='{name}'", connection);

            await connection.OpenAsync();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Person person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }

            reader.Close();
            connection.Close();
            return people;
        }

        public async Task<bool> CreatePersonAsync(Person person)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={person.Id} ", connection);
            await connection.OpenAsync();

            SqlDataReader reader =await commandForID.ExecuteReaderAsync();
            
            if (person.Id <= 0)
            {
                return false;
            }

            else if (reader.HasRows)
            { 
                return false;
            }

            reader.Close();
            SqlCommand command = new SqlCommand($"INSERT INTO Person (ID, Name , Surname , City ) VALUES ({person.Id},'{person.Name}','{person.Surname}',{person.City})", connection);
            
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> UpdatePersonAsync(int id,Person person)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }
           
            SqlCommand command = new SqlCommand($"UPDATE Person SET City={person.City}, Name='{person.Name}', Surname='{person.Surname}' WHERE ID={id}", connection);
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand($"DELETE FROM Person WHERE ID={id}; ", connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }
    }


}
