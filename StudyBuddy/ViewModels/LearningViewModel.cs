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
	private bool visibilityCheck;

	[ObservableProperty]
	public CategoryModel selectedCategory; 

	[ObservableProperty]
	public List<CategoryModel> categories;
	[ObservableProperty]
	public List<FlashCardModel> flashcards;
	[ObservableProperty]
	public List<FlashCardModel> correctflashcardanswer;

	public LearningViewModel()
	{
		_databaseService = new DatabaseService();

		Categories = new List<CategoryModel>();
		Flashcards = new List<FlashCardModel>();
		Correctflashcardanswer = new List<FlashCardModel>();

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
		VisibilityCheck = false;
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
			Flashcards = _databaseService.GetFlashCards();
			if(Flashcards.Count == Correctflashcardanswer.Count)
			{
				await Shell.Current.DisplayAlert("Random Flashcard", "You have answered all flash cards", "OK");

				Correctflashcardanswer.Clear();
				await GetRandomFlashCard();
			}
			else
			{
				//get random flashcard from database where the category is the selected category
				Flashcards = _databaseService.GetRandomFlashCard(SelectedCategory.CategoryName);
				if (Flashcards.Count > 0)
				{
					//get random number between 0 and the number of flashcards in the list
					var random = new Random();
					var randomNumber = random.Next(0, Flashcards.Count);

					//check if the question is already in the correctflashcardanswer list
					if (Correctflashcardanswer.FirstOrDefault(x => x.Question == Flashcards[randomNumber].Question) != null)
					{
						//if the question is already in the list, get a new random flashcard
						await GetRandomFlashCard();
					}
					else
					{
						Question = Flashcards[randomNumber].Question;
						Answer = Flashcards[randomNumber].Answer;
					}
				}
				else
				{
					await Shell.Current.DisplayAlert("Random Flashcard", "No flashcards found", "OK");
				}
			}
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	[RelayCommand]
	public void CorrectAnswer()
	{
		//add the answer to the list correctflashcardanswer
		Correctflashcardanswer.Add(new FlashCardModel { Question = Question, Answer = Answer });
		VisibilityCheck = true;
	}

	[RelayCommand]
	public void VisibleAnswer()
	{
		VisibilityAnswer = true;
	}
}
