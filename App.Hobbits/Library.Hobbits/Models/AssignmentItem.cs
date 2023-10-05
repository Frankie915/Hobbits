using System;

namespace Library.Hobbits.Models
{
    public class AssignmentItem : ContentItem
    {
        public Assignment? Assignment { get; set; }
        public override string ToString()
        {
            return $"{base.ToString()}\n{Assignment}";
        }
    }
}

