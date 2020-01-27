using CsvHelper.Configuration;

namespace src.SampleData
{
    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.LastName).Index(0);
            Map(m => m.Name).Index(1);
            Map(m => m.Direction).Index(2);
            Map(m => m.Color).Index(3);            
        }
    }
}