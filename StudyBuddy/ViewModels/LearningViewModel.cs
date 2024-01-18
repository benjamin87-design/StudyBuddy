namespace StudyBuddy.ViewModels;

public partial class LearningViewModel : BaseViewModel
{
	private DatabaseService _databaseService;

	[ObservableProperty]
	private string question;
	[ObservableProperty]
	private string answer;
	[ObservableProperty]
	private string categoryName;
	[ObservableProperty]
	private bool visibilityAnswer;

	[ObservableProperty]
	public CategoryModel selectedCategory; 

	[ObservableProperty]
	public List<CategoryModel> categories;
	[ObservableProperty]
	public List<FlashCardModel> flashcards;

	public LearningViewModel()
	{
		_databaseService = new DatabaseService();

		Categories = new List<CategoryModel>();
		GetAllCategories();
		VisibilityAnswer = false;
	}

	public void ClearStrings()
	{
		Question = "";
		Answer = "";
		CategoryName = "";
		Flashcards.Clear();
		VisibilityAnswer = false;
	}
	
	public async void GetAllCategories()
	{
		Categories.Clear();

		try
		{
			Categories = _databaseService.GetCategories();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	[RelayCommand]
	public async Task GetRandomFlashCard()
	{
		ClearStrings();

		try
		{
			//get random flashcard from database where the category is the selected category
			Flashcards = _databaseService.GetRandomFlashCard(SelectedCategory.CategoryName);
			if (Flashcards.Count > 0)
			{
				//get random number between 0 and the number of flashcards in the list
				var random = new Random();
				var randomNumber = random.Next(0, Flashcards.Count);

				Question = Flashcards[randomNumber].Question;
				Answer = Flashcards[randomNumber].Answer;
			}
			else
			{
				await Shell.Current.DisplayAlert("Random Flashcard", "No flashcards found", "OK");
			}
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	[RelayCommand]
	public async Task VisibleAnswer()
	{
		VisibilityAnswer = true;
	}
}
