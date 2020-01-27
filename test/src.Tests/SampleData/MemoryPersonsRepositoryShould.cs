using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using src.SampleData;
using Xunit;

namespace test.src.SampleData
{
    public class MemoryPersonsRepositoryShould
    {
        [Fact]
        public async Task ReturnAllPersonsCorrectly()
        {
            var expectedPersons = new List<Person>()
            {
                CreateTestPerson(1, "James", "Gordon", "12345 Gotham", Color.Blue),
                CreateTestPerson(2, "Bruce", "Wayne", "83731 Gotham", Color.White)
            };
            var repository = new MemoryPersonsRepository();
            await PutTestingInformationInRepository(repository, expectedPersons);

            var actualPersons = await repository.GetAllPersonsAsync();

            Assert.Equal(JsonConvert.SerializeObject(expectedPersons), JsonConvert.SerializeObject(actualPersons));
        }

        [Fact]
        public async Task ReturnPersonByIdCorrectly()
        {
            var expectedId = 3;
            var expectedPerson = CreateTestPerson(expectedId, "James", "Gordon", "12345 Gotham", Color.Blue);
            var persons = new List<Person>()
            {
                CreateTestPerson(1, "Bruce", "Wayne", "83731 Gotham", Color.White),
                CreateTestPerson(2, "Alfred", "Pennyworth", "83731 Gotham", Color.Violett),
                expectedPerson
            };
            var repository = new MemoryPersonsRepository();
            await PutTestingInformationInRepository(repository, persons);

            var actualPerson = await repository.GetPersonByIdAsync(expectedId);

            Assert.Equal(JsonConvert.SerializeObject(expectedPerson), JsonConvert.SerializeObject(actualPerson));
        }

        [Fact]
        public async Task ReturnAllPersonByColorCorrectly()
        {
            var expectedColor = Color.Green;
            var expectedPerson1 = CreateTestPerson(1, "James", "Gordon", "12345 Gotham", expectedColor);
            var expectedPerson2 = CreateTestPerson(2, "Harvey", "Bullock", "93721 Metropolis", expectedColor);
            var persons = new List<Person>()
            {
                CreateTestPerson(3, "Bruce", "Wayne", "83731 Gotham", Color.White),
                CreateTestPerson(4, "Alfred", "Pennyworth", "83731 Gotham", Color.Violett),
                expectedPerson1,
                expectedPerson2
            };
            var expectedPersons = new List<Person>() { expectedPerson1, expectedPerson2 };
            var repository = new MemoryPersonsRepository();
            await PutTestingInformationInRepository(repository, expectedPersons);

            var actualPersons = await repository.GetPersonsByColorAsync(expectedColor);

            Assert.Equal(JsonConvert.SerializeObject(expectedPersons), JsonConvert.SerializeObject(actualPersons));
        }
        

        private Person CreateTestPerson(int id, string name, string lastName, string direction, Color color) =>
            new Person()
            {
                Id = id,
                Name = name,
                LastName = lastName,
                Direction = direction,
                Color = color
            };

        private async Task PutTestingInformationInRepository(
            MemoryPersonsRepository repository,
            IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                await repository.SetPersonAsync(person);
            }
        }
    }
}