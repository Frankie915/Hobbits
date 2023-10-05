namespace Library.Hobbits.Models
{
    public class Module
    {
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
