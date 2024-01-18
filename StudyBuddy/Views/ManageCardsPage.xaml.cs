namespace StudyBuddy.Views;

public partial class ManageCardsPage : ContentPage
{
	public ManageCardsPage(ManageCardsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
