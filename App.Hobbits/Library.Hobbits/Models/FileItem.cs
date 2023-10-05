using System;

namespace Library.Hobbits.Models
{
    public class FileItem : ContentItem
    {
        public string? Path { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}\n\t{Path}";
        }
    } 
}

