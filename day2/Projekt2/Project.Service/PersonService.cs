using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service
{
    public class PersonService
    {
        PersonRepository personRepository = new PersonRepository();
        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await personRepository.GetAllPersonAsync();
            
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await personRepository.GetPersonByIdAsync(id);
        }

        public async Task<List<Person>> GetPersonByNameAsync(string name)
        {
            return await personRepository.GetPersonByNameAsync(name);
        }

        public async Task<bool> CreatePersonAsync(Person person)
        {
            return await personRepository.CreatePersonAsync(person);
           
        }

        public async Task<bool> UpdatePersonAsync(int id,Person person)
        {
            return await personRepository.UpdatePersonAsync(id, person);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            return await personRepository.DeletePersonAsync(id);
        }
    }
}
