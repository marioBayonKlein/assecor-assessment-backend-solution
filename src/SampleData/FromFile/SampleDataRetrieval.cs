using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using src.SampleData.Common;

namespace src.SampleData.FromFile
{
    public class SampleDataRetrieval : ISampleDataRetrieval
    {
        private readonly FileOptions options;
        public SampleDataRetrieval(IOptions<FileOptions> options)
        {
            this.options = options.Value;
        }
        public Task<IEnumerable<PersonFromSource>> ReturnPersonsFromSampleDataAsync()
        {
            var persons = ReadPersonsFromFile();

            return Task.FromResult(persons);
        }

        private IEnumerable<PersonFromSource> ReadPersonsFromFile()
        {
            var results = new List<PersonFromSource>();
            using (var reader = new StreamReader(options.Path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<PersonMap>();

                var records = csv.GetRecords<PersonFromSource>();
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