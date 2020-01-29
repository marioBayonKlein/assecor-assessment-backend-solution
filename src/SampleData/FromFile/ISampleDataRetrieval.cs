using System.Collections.Generic;
using System.Threading.Tasks;
using src.SampleData.Common;

namespace src.SampleData.FromFile
{
    public interface ISampleDataRetrieval
    {
        Task<IEnumerable<PersonFromSource>> ReturnPersonsFromSampleDataAsync();
    }
}