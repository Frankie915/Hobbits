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
        var context = BindingContext as PersonDetailViewModel;
        PersonClassification classification;
        switch (context.ClassificationString)
        {
            case "S":
                classification = PersonClassification.Senior;
                break;
            case "J":
                classification = PersonClassification.Junior;
                break;
            case "O":
                classification = PersonClassification.Sophomore;
                break;
            case "F":
            default:
                classification = PersonClassification.Senior;
                break;
        }
        StudentService.Current.Add(new Student { Name = context.Name, Classification = classification });
        Shell.Current.GoToAsync("//MainPage");
    }
}