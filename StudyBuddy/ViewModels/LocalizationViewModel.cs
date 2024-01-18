namespace StudyBuddy.ViewModels;

public partial class LocalizationViewModel : BaseViewModel
{
	public string LocalizedText => StudyBuddy.Resources.Strings.AppResources.HelloMessage;
}
