namespace Library.Hobbits.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }

    public enum PersonClassification
    {
        Freshmen, Sophomore, Junior, Senior
    }
}