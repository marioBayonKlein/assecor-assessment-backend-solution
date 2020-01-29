using CsvHelper.Configuration;
using src.SampleData.Common;

namespace src.SampleData.FromFile
{
    public sealed class PersonMap : ClassMap<PersonFromSource>
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