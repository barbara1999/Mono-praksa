using Project.Model;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service
{
    public class PersonService : IPersonService
    {
        IPersonRepository personRepository = new PersonRepository();
        public async Task<List<IPerson>> GetAllPeopleAsync()
        {
            return await personRepository.GetAllPersonAsync();

        }

        public async Task<IPerson> GetPersonByIdAsync(int id)
        {
            return await personRepository.GetPersonByIdAsync(id);
        }

        public async Task<List<IPerson>> GetPersonByNameAsync(string name)
        {
            return await personRepository.GetPersonByNameAsync(name);
        }

        public async Task<bool> CreatePersonAsync(IPerson person)
        {
            return await personRepository.CreatePersonAsync(person);

        }

        public async Task<bool> UpdatePersonAsync(int id, IPerson person)
        {
            return await personRepository.UpdatePersonAsync(id, person);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            return await personRepository.DeletePersonAsync(id);
        }
    }
}
