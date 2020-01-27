using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.SampleData;
using Xunit;

namespace test.src.SampleData
{
    public class SampleDataRetrievalShould
    {
        [Fact]
        public async Task ReturnSampleDataFromFile()
        {
            var expectedData = CreateExpectedSampleData();
            var options = new FileOptions()
            {
                Path = "../../../SampleData/sample-input-test.csv"
            };

            var dataRetrieval = new SampleDataRetrieval(options);
            var actualData = await dataRetrieval.ReturnPersonsFromSampleDataAsync();

            Assert.Equal(JsonConvert.SerializeObject(expectedData), JsonConvert.SerializeObject(actualData));
        }

        private IEnumerable<Person> CreateExpectedSampleData()
            => new List<Person>()
                {
                    new Person()
                    {
                        Id = 1,
                        Name = "Michael",
                        LastName = "Schumacher",
                        Direction = "12345 Frankfurt",
                        Color = Color.Blue
                    },
                    new Person()
                    {
                        Id = 2,
                        Name = "Lewis",
                        LastName = "Hamilton",
                        Direction = "98765 London",
                        Color = Color.Red
                    },
                    new Person()
                    {
                        Id = 3,
                        Name = "Nico",
                        LastName = "Hülkenberg",
                        Direction = "82372 Abu Dabi",
                        Color = Color.Violett
                    }
                };
    }
}