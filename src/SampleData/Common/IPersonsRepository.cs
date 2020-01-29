using System.Collections.Generic;
using System.Threading.Tasks;

namespace src.SampleData.Common
{
    public interface IPersonsRepository
    {
        Task<IEnumerable<PersonFromSource>> GetAllPersonsAsync();
        Task<PersonFromSource> GetPersonByIdAsync(int id);
        Task<IEnumerable<PersonFromSource>> GetPersonsByColorAsync(Color color);
        Task SetPersonAsync(PersonFromSource person);
    }
}