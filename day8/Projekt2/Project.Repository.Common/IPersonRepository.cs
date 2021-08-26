using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Common;
using Project.Model;
using Project.Model.Common;

namespace Project.Repository.Common
{
    public interface IPersonRepository
    {
        Task<bool> CreatePersonAsync(IPerson person);
        Task<bool> DeletePersonAsync(int id);
        Task<List<IPerson>> GetAllPersonAsync(Sorter sorter, Pager pager, PersonFilter filter);
        Task<IPerson> GetPersonByIdAsync(int id);
        Task<List<IPerson>> GetPersonByNameAsync(string name);
        Task<bool> UpdatePersonAsync(int id, IPerson person);
    }
}
