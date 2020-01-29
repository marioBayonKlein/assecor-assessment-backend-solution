using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.SampleData.Common;

namespace src.SampleData.FromDataBase
{
    public class SqlServerAccessRepository : IPersonsRepository
    {
        private readonly DBContext context;
        public SqlServerAccessRepository(DBContext context)
        {
            this.context = context;
        }        

        public async Task<IEnumerable<PersonFromSource>> GetAllPersonsAsync()
        {  
            var persons = await context.Persons.ToListAsync();

            return persons;
        }

        public Task<PersonFromSource> GetPersonByIdAsync(int id)
        {
            var person = context.Persons.SingleOrDefault(p => p.Id == id);

            return Task.FromResult(person);
        }

        public Task<IEnumerable<PersonFromSource>> GetPersonsByColorAsync(Color color) =>
            Task.FromResult(GetPersonsByColor(color));

        private IEnumerable<PersonFromSource> GetPersonsByColor(Color color)
        {
            return context.Persons
                .Where(p => p.Color == color)
                .ToList();
        }

        public Task SetPersonAsync(PersonFromSource person)
        {
            context.Persons.Add(person);
            
            return context.SaveChangesAsync();
        }
    }   
}