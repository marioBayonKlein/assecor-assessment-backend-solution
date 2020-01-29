using System.Collections.Generic;
using System.Threading.Tasks;
using src.SampleData.Common;

namespace app
{
    public interface ISampleDataManager
    {
        Task<IEnumerable<PersonResponse>> GetPersonsResponseAsync();
        Task<PersonResponse> GetPersonResponseByIdAsync(int id);
        Task<IEnumerable<PersonResponse>> GetPersonsResponseByColorAsync(Color color);
        Task SetPersonResponseAsync(PersonResponse personResponse);
        Task InitializeRepositoryAsync();
    }
}