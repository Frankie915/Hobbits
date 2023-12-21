using Library.Hobbits.Models;
using Library.Hobbits.Services;
using MAUI.Hobbits.ViewModels;

namespace MAUI.Hobbits.Views;

public partial class PersonDetailView : ContentPage
{
    public PersonDetailView()
    {
        InitializeComponent();

        BindingContext = new PersonDetailViewModel();
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as PersonDetailViewModel).AddPerson();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e) 
    {
        BindingContext = new PersonDetailViewModel();
    }
}