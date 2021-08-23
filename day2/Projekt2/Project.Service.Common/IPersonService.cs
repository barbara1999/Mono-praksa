using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IPersonService
    {
        Task<bool> CreatePersonAsync(IPerson person);
        Task<bool> DeletePersonAsync(int id);
        Task<List<IPerson>> GetAllPeopleAsync();
        Task<IPerson> GetPersonByIdAsync(int id);
        Task<List<IPerson>> GetPersonByNameAsync(string name);
        Task<bool> UpdatePersonAsync(int id, IPerson person);
    }
}
