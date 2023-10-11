namespace Library.Hobbits.Models
{
    public class Module
    {
        private static int lastId = 0;
        private int id = 0;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ContentItem> Content { get; set; }
        
        public Module() { 
            Content = new List<ContentItem>();
        }

        public override string ToString()
        {
            return $"{Name}: {Description}\n" + 
                $"\t{string.Join("\n\t", Content.Select(c => c.ToString()).ToArray())}"; 
        }
    }
}
