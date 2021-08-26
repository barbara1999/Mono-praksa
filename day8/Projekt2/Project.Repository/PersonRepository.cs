using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Project.Model;
using System.Threading.Tasks;
using Project.Repository.Common;
using Project.Model.Common;
using Project.Common;

namespace Project.Repository
{
    public class PersonRepository : IPersonRepository

    {
        SqlConnection _connection;
        public PersonRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<IPerson>> GetAllPersonAsync(Sorter sorter, Pager pager, PersonFilter filter)
        {

            SqlCommand command = new SqlCommand("SELECT * FROM Person"+sorter.SQLMethod()+filter.SQlMethod()+pager.getSQL(), _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<IPerson> people = new List<IPerson>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Person person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    people.Add(person);
                }
            }
            reader.Close();
            _connection.Close();
            return people;
        }

        public async Task<IPerson> GetPersonByIdAsync(int id)
        {

            Person person = new Person();
            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", _connection);

            await _connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                }
            }

            reader.Close();
            _connection.Close();
            return person;
        }

        public async Task<List<IPerson>> GetPersonByNameAsync(string name)
        {
            List<IPerson> people = new List<IPerson>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE Name='{name}'", _connection);

            await _connection.OpenAsync();
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
            _connection.Close();
            return people;
        }

        public async Task<bool> CreatePersonAsync(IPerson person)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={person.Id} ", _connection);
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (person.Id <= 0)
            {
                return false;
            }

            else if (reader.HasRows)
            {
                return false;
            }

            reader.Close();
            SqlCommand command = new SqlCommand($"INSERT INTO Person (ID, Name , Surname , City ) VALUES ({person.Id},'{person.Name}','{person.Surname}',{person.City})", _connection);

            await command.ExecuteNonQueryAsync();
            _connection.Close();
            return true;
        }

        public async Task<bool> UpdatePersonAsync(int id, IPerson person)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", _connection);
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            SqlCommand command = new SqlCommand($"UPDATE Person SET City={person.City}, Name='{person.Name}', Surname='{person.Surname}' WHERE ID={id}", _connection);
            await command.ExecuteNonQueryAsync();
            _connection.Close();
            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            SqlCommand commandForID = new SqlCommand($"SELECT * FROM Person WHERE ID={id} ", _connection);
            SqlCommand command = new SqlCommand();
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand($"DELETE FROM Person WHERE ID={id}; ", _connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            _connection.Close();
            return true;
        }
    }


}
