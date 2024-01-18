namespace StudyBuddy.Views;

public partial class LearningPage : ContentPage
{
	public LearningPage(LearningViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
