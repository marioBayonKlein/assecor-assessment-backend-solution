using System.Collections.Generic;
using System.Threading.Tasks;

namespace src.SampleData
{
    public interface ISampleDataRetrieval
    {
        Task<IEnumerable<Person>> ReturnPersonsFromSampleDataAsync();
    }
}