using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace src.SampleData
{
    public class SampleDataRetrieval : ISampleDataRetrieval
    {
        private readonly FileOptions options;
        public SampleDataRetrieval(FileOptions options)
        {
            this.options = options;
        }
        public Task<IEnumerable<Person>> ReturnPersonsFromSampleDataAsync()
        {
            var persons = ReadPersonsFromFile();

            return Task.FromResult(persons);
        }

        private IEnumerable<Person> ReadPersonsFromFile()
        {
            var results = new List<Person>();
            using (var reader = new StreamReader(options.Path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<PersonMap>();

                var records = csv.GetRecords<Person>();
                var idIndex = 1;
                
                foreach(var record in records)
                {
                    record.Id = idIndex++;
                    record.Name = record.Name.Trim();
                    record.LastName = record.LastName.Trim();
                    record.Direction = record.Direction.Trim();
                    results.Add(record);
                }
            }

            return results;
        }
    }
}