using MAUI.Hobbits.ViewModels;

namespace MAUI.Hobbits.Views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
		BindingContext = new StudentViewViewModel();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}