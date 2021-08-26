using Project.Common;
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
        IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<List<IPerson>> GetAllPeopleAsync(Sorter sorter, Pager pager, PersonFilter filter)
        {
            return await _personRepository.GetAllPersonAsync(sorter,pager,filter);

        }

        public async Task<IPerson> GetPersonByIdAsync(int id)
        {
            return await _personRepository.GetPersonByIdAsync(id);
        }

        public async Task<List<IPerson>> GetPersonByNameAsync(string name)
        {
            return await _personRepository.GetPersonByNameAsync(name);
        }

        public async Task<bool> CreatePersonAsync(IPerson person)
        {
            return await _personRepository.CreatePersonAsync(person);

        }

        public async Task<bool> UpdatePersonAsync(int id, IPerson person)
        {
            return await _personRepository.UpdatePersonAsync(id, person);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            return await _personRepository.DeletePersonAsync(id);
        }
    }
}
