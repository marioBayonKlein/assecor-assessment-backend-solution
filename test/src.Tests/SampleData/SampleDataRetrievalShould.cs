using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using src.SampleData.Common;
using src.SampleData.FromFile;
using Xunit;

namespace test.src.SampleData
{
    public class SampleDataRetrievalShould
    {
        [Fact]
        public async Task ReturnSampleDataFromFile()
        {
            var expectedData = CreateExpectedSampleData();

            var options = Options.Create<FileOptions>(new FileOptions()
            {
                Path = "../../../SampleData/sample-input-test.csv"
            });

            var dataRetrieval = new SampleDataRetrieval(options);
            var actualData = await dataRetrieval.ReturnPersonsFromSampleDataAsync();

            Assert.Equal(JsonConvert.SerializeObject(expectedData), JsonConvert.SerializeObject(actualData));
        }

        private IEnumerable<PersonFromSource> CreateExpectedSampleData()
            => new List<PersonFromSource>()
                {
                    new PersonFromSource()
                    {
                        Id = 1,
                        Name = "Michael",
                        LastName = "Schumacher",
                        Direction = "12345 Frankfurt",
                        Color = Color.Blue
                    },
                    new PersonFromSource()
                    {
                        Id = 2,
                        Name = "Lewis",
                        LastName = "Hamilton",
                        Direction = "98765 London",
                        Color = Color.Red
                    },
                    new PersonFromSource()
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