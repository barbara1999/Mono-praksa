using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;

namespace Project.Service
{
    public class PersonService
    {
        PersonRepository personRepository = new PersonRepository();
        public List<Person> GetAllPeople()
        {
            return personRepository.GetAllPerson();
            
        }

        public Person GetPersonById(int id)
        {
            return personRepository.GetPersonById(id);
        }

        public List<Person> GetPersonByName(string name)
        {
            return personRepository.GetPersonByName(name);
        }

        public void CreatePerson(Person person)
        {
            personRepository.CreatePerson(person);
        }

        public bool UpdatePerson(int id,Person person)
        {
            return personRepository.UpdatePerson(id, person);
        }

        public bool DeletePerson(int id)
        {
            return personRepository.DeletePerson(id);
        }
    }
}
