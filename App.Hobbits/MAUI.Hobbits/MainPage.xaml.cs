using MAUI.Hobbits.ViewModels;

namespace MAUI.Hobbits
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void StudentClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Student");
        }

        private void InstructorClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Instructor");
        }

    }
}