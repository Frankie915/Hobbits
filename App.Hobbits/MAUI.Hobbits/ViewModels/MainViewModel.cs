using Library.Hobbits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Hobbits.ViewModels
{
    internal class MainViewModel
    {
        public List<Person> Students { get; set; } = new List<Person>();
        public MainViewModel() { }
    }
}
