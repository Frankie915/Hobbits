using Library.Hobbits.Models;
using Library.Hobbits.Services;
using MAUI.Hobbits.ViewModels;

namespace MAUI.Hobbits.Views;

[QueryProperty(nameof(PersonId), "personId")]

public partial class PersonDetailView : ContentPage
{
    public PersonDetailView()
    {
        InitializeComponent();
    }

    public int PersonId { set; get; }

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
        BindingContext = new PersonDetailViewModel(PersonId);
    }
}