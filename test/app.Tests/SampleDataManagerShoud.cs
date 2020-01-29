using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app;
using Moq;
using Newtonsoft.Json;
using src.SampleData;
using Xunit;

namespace test.app
{
    public class SampleDataManagerShould
    {
        [Fact]
        public async Task GetPersonResponseByIdCorrectly()
        {
            var expectedId = 1;
            var expectedPersonResponse = new PersonResponse()
            {
                Id = expectedId,
                Name = "Michael",
                LastName = "Faraday",
                ZipCode = "123456",
                City = "Berlin",
                Color = Color.Green.ToString()
            };
            var repository = CreateMockOfRepositoryForIdCase(expectedPersonResponse);
            var sampleDataRetrieval = new Mock<ISampleDataRetrieval>().Object;

            var manager = new SampleDataManager(
                repository,
                sampleDataRetrieval);
            var actualPerson = await manager.GetPersonResponseByIdAsync(expectedId);

            Assert.Equal(JsonConvert.SerializeObject(expectedPersonResponse), JsonConvert.SerializeObject(actualPerson));
        }

        [Fact]
        public async Task GetPersonsResponseCorrectly()
        {
            var expectedPersonsResponse = CreateExpectedPersonsResponse();
            var repository = CreateMockOfRepositoryForAllPersonsCase(expectedPersonsResponse);
            var sampleDataRetrieval = new Mock<ISampleDataRetrieval>().Object;

            var manager = new SampleDataManager(
                repository,
                sampleDataRetrieval);
            var actualPersons = await manager.GetPersonsResponseAsync();

            Assert.Equal(JsonConvert.SerializeObject(expectedPersonsResponse), JsonConvert.SerializeObject(actualPersons));
        }

        [Fact]
        public async Task GetPersonsResponseByColorCorrectly()
        {
            var expectedColor = Color.Green;
            var expectedPersonsResponse = CreateExpectedPersonsResponse()
                .Where(p => p.Color == expectedColor.ToString());
            var repository = CreateMockOfRepositoryColorPersonCase(expectedPersonsResponse, expectedColor);
            var sampleDataRetrieval = new Mock<ISampleDataRetrieval>().Object;

            var manager = new SampleDataManager(
                repository,
                sampleDataRetrieval);
            var actualPersons = await manager.GetPersonsResponseByColorAsync(expectedColor);

            Assert.Equal(JsonConvert.SerializeObject(expectedPersonsResponse), JsonConvert.SerializeObject(actualPersons));
        }

        [Fact]
        public async Task SetPersonResponseCorrectly()
        {
            var personResponse = new PersonResponse()
            {
                Id = 1,
                Name = "Peter",
                LastName = "Parker",
                ZipCode = "65829",
                City = "New York",
                Color = Color.Red.ToString()
            };
            var expectedPerson = ParseToPerson(personResponse);
            var repository = new Mock<IPersonsRepository>();
            repository.Setup(r => r.SetPersonAsync(expectedPerson))
                .Returns(Task.CompletedTask);
            var sampleDataRetrieval = new Mock<ISampleDataRetrieval>().Object;

            var manager = new SampleDataManager(
                repository.Object,
                sampleDataRetrieval);
            await manager.SetPersonResponseAsync(personResponse);

            repository.Verify(r => r.SetPersonAsync(It.IsAny<Person>()), Times.Once);

            Person ParseToPerson(PersonResponse personResponse)
            {
                return new Person()
                {
                    Id = personResponse.Id,
                    Name = personResponse.Name,
                    LastName = personResponse.LastName,
                    Color = (Color)Enum.Parse(typeof(Color), personResponse.Color),
                    Direction = personResponse.ZipCode + " " + personResponse.City
                };
            }
        }

        [Fact]
        public async Task InitializeRepositoryCorrectly()
        {
            var retrievedPersons = ReturnEnumerableRetrievedPersons;
            var sampleDataRetrieval = new Mock<ISampleDataRetrieval>();
            sampleDataRetrieval.Setup(s => s.ReturnPersonsFromSampleDataAsync())
               .Returns(Task.FromResult(retrievedPersons));

            var repository = new Mock<IPersonsRepository>();

            var manager = new SampleDataManager(
                repository.Object,
                sampleDataRetrieval.Object);
            await manager.InitializeRepositoryAsync();

            repository.Verify(r => r.SetPersonAsync(It.IsAny<Person>()), Times.AtMost(2));
        }

        private IPersonsRepository CreateMockOfRepositoryForIdCase(PersonResponse personResponse)
        {
            var repository = new Mock<IPersonsRepository>();
            repository.Setup(r => r.GetPersonByIdAsync(personResponse.Id))
                .Returns(Task.FromResult(ParseToSourcePerson(personResponse)));

            return repository.Object;
        }

        private IPersonsRepository CreateMockOfRepositoryForAllPersonsCase(IEnumerable<PersonResponse> personsResponse)
        {
            var repository = new Mock<IPersonsRepository>();
            var parsedPersons = personsResponse.Select(ParseToSourcePerson);

            repository.Setup(r => r.GetAllPersonsAsync())
                .Returns(Task.FromResult(parsedPersons));

            return repository.Object;
        }

        private IPersonsRepository CreateMockOfRepositoryColorPersonCase(
            IEnumerable<PersonResponse> personsResponse,
            Color expectedColor)
        {
            var repository = new Mock<IPersonsRepository>();
            var parsedPersons = personsResponse.Select(ParseToSourcePerson);

            repository.Setup(r => r.GetPersonsByColorAsync(expectedColor))
                .Returns(Task.FromResult(parsedPersons));

            return repository.Object;
        }

        private IEnumerable<PersonResponse> CreateExpectedPersonsResponse()
            => new List<PersonResponse>()
            {
                    new PersonResponse()
                {
                    Id = 1,
                    Name = "Peter",
                    LastName = "Parker",
                    ZipCode = "65829",
                    City = "New York",
                    Color = Color.Red.ToString()
                },
                    new PersonResponse()
                {
                    Id = 2,
                    Name = "Eddy",
                    LastName = "Brock",
                    ZipCode = "93023",
                    City = "San Francisco",
                    Color = Color.Green.ToString()
                },
                    new PersonResponse()
                {
                    Id = 3,
                    Name = "Norman",
                    LastName = "Osborne",
                    ZipCode = "82711",
                    City = "New York",
                    Color = Color.Green.ToString()
                }
            };

        private static IEnumerable<Person> ReturnEnumerableRetrievedPersons =>
        new List<Person>()
        {
                new Person(),
                new Person()
        };

        private Person ParseToSourcePerson(PersonResponse personResponse) =>
            new Person()
            {
                Id = personResponse.Id,
                Name = personResponse.Name,
                LastName = personResponse.LastName,
                Color = (Color)Enum.Parse(typeof(Color), personResponse.Color),
                Direction = personResponse.ZipCode + " " + personResponse.City
            };
    }
}