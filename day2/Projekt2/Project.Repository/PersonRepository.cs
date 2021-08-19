using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Project.Model;

namespace Project.Repository
{
   
    public class PersonRepository


    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString);

        public List<Person> GetAllPerson()
        {

            SqlCommand command = new SqlCommand("SELECT * FROM Person", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Person> people = new List<Person>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }
            reader.Close();
            connection.Close();
            return people;
        }

        public Person GetPersonById(int id)
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
           
            reader.Close();
            connection.Close();
            return person;
        }

        public List<Person> GetPersonByName(string name)
        {
            List<Person> people = new List<Person>();
           
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE Name='{name}'", connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }

            reader.Close();
            connection.Close();
            return people;
        }

        public void CreatePerson(Person person)
        {
            Person mPerson = new Person();
            mPerson.Id = person.Id;
            mPerson.Name = person.Name;
            mPerson.Surname = person.Surname;
            mPerson.City = person.City;

            SqlCommand command = new SqlCommand($"INSERT INTO Person (ID, Name , Surname , City ) VALUES ({mPerson.Id},'{mPerson.Name}','{mPerson.Surname}',{mPerson.City})", connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
 
        }

        public bool UpdatePerson(int id,Person person)
        {
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
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand($"UPDATE Person SET City={mPerson.City}, Name='{mPerson.Name}', Surname='{mPerson.Surname}' WHERE ID={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeletePerson(int id)
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
                return false;
            }
            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
    }


}
