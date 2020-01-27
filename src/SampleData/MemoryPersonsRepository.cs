using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.SampleData
{
    public class MemoryPersonsRepository: IPersonsRepository
    {
        private IDictionary<int, Person> AllPersonsById
            = new Dictionary<int, Person>();
        private IEnumerable<Person> AllPersons
            => AllPersonsById.Values;

        public Task<IEnumerable<Person>> GetAllPersonsAsync() 
            => Task.FromResult(AllPersons);


        public Task<Person> GetPersonByIdAsync(int id)
        {
            AllPersonsById.TryGetValue(id, out var person);

            return Task.FromResult(person);
        }
        public Task<IEnumerable<Person>> GetPersonsByColorAsync(Color color)
        {
            var persons = AllPersons.Where( p => p.Color == color);

            return Task.FromResult(persons);
        }

        public Task SetPersonAsync(Person person)
        {
            AllPersonsById.Add(person.Id, person);

            return Task.CompletedTask;
        }
    }
}