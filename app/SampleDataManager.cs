using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.SampleData.Common;
using src.SampleData.FromFile;

namespace app
{
    public class SampleDataManager : ISampleDataManager
    {
        private readonly IPersonsRepository personsRepository;
        private readonly ISampleDataRetrieval sampleDataRetrieval;

        public SampleDataManager(
            IPersonsRepository personsRepository,
            ISampleDataRetrieval sampleDataRetrieval)
        {
            this.personsRepository = personsRepository;
            this.sampleDataRetrieval = sampleDataRetrieval;
        }
        public async Task<PersonResponse> GetPersonResponseByIdAsync(int id)
        {
            var person = await personsRepository.GetPersonByIdAsync(id);

            return ReturnPersonResponseFormat(person);
        }

        public async Task<IEnumerable<PersonResponse>> GetPersonsResponseAsync()
        {
            var persons = await personsRepository.GetAllPersonsAsync();

            return persons.Select(ReturnPersonResponseFormat);
            
        }

        public async Task<IEnumerable<PersonResponse>> GetPersonsResponseByColorAsync(Color color)
        {
            var persons = await personsRepository.GetPersonsByColorAsync(color);

            return persons.Select(ReturnPersonResponseFormat);
        }

        public async Task SetPersonResponseAsync(PersonResponse personResponse)
        {
            await personsRepository.SetPersonAsync(ParseToPerson(personResponse));

            PersonFromSource ParseToPerson(PersonResponse personResponse)
            {
                return new PersonFromSource()
                {
                    Name = personResponse.Name,
                    LastName = personResponse.LastName,
                    Color = (Color)Enum.Parse(typeof(Color), personResponse.Color),
                    Direction = personResponse.ZipCode + " " + personResponse.City
                };
            }
        }

        public async Task InitializeRepositoryAsync()
        {
            var retrievedPersons = await sampleDataRetrieval.ReturnPersonsFromSampleDataAsync();

            foreach (PersonFromSource retrievedPerson in retrievedPersons)
            {
                await personsRepository.SetPersonAsync(retrievedPerson);
            }
        }

        private PersonResponse ReturnPersonResponseFormat(PersonFromSource person)
        {
            if (person == null)
            {
                return null;
            }

            var firstSpaceIndex = person.Direction.IndexOf(" ");
            return new PersonResponse()
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                ZipCode = person.Direction.Substring(0, firstSpaceIndex),
                City = person.Direction.Substring(firstSpaceIndex + 1),
                Color = person.Color.ToString()
            };
        }
    }
}