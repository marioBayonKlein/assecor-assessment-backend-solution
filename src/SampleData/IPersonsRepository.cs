using System.Collections.Generic;
using System.Threading.Tasks;

namespace src.SampleData
{
    public interface IPersonsRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> GetPersonsByColorAsync(Color color);
        Task SetPersonAsync(Person person);
    }
}