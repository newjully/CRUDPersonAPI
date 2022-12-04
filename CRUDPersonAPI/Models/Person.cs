namespace CRUDPersonAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Person> People { get; set; }

        public Person()
        {
            People = new List<Person>();
        }

        public void Add(Person person)
        {
            People.Add(person);
        }
        public void Update(Person person)
        {
            Remove(person.Id);
            People.Add(person);
        }

        public void Remove(long id)
        {
            Person person = People.FirstOrDefault(p => p.Id == id);
            People.Remove(person);
        }
    }
}