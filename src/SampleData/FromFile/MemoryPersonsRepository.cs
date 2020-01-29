using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.SampleData.Common;

namespace src.SampleData.FromFile
{
    public class MemoryPersonsRepository: IPersonsRepository
    {
        private IDictionary<int, PersonFromSource> AllPersonsById
            = new Dictionary<int, PersonFromSource>();
        private IEnumerable<PersonFromSource> AllPersons
            => AllPersonsById.Values;

        public Task<IEnumerable<PersonFromSource>> GetAllPersonsAsync() 
            => Task.FromResult(AllPersons);


        public Task<PersonFromSource> GetPersonByIdAsync(int id)
        {
            AllPersonsById.TryGetValue(id, out var person);

            return Task.FromResult(person);
        }
        public Task<IEnumerable<PersonFromSource>> GetPersonsByColorAsync(Color color)
        {
            var persons = AllPersons.Where( p => p.Color == color);

            return Task.FromResult(persons);
        }

        public Task SetPersonAsync(PersonFromSource person)
        {
            AllPersonsById.Add(person.Id, person);

            return Task.CompletedTask;
        }
    }
}