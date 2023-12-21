using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Hobbits.ViewModels
{
    public class InstructorViewViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(StudentService.Current.Students);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddClick(Shell s)
        {
            s.GoToAsync("//PersonDetail");
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
        }
    }
}
