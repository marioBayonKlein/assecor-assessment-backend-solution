namespace src.SampleData
{
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public Color Color { get; set; }

        public string ZipCode
        {
            get
            {
                var firstSpaceIndex = Direction.IndexOf(" ");
                return Direction.Substring(0, firstSpaceIndex);
            }
            set { }
        }
        public string City
        {
            get
            {
                var firstSpaceIndex = Direction.IndexOf(" ");
                return Direction.Substring(firstSpaceIndex + 1);
            }
            set { }
        }
    }
}