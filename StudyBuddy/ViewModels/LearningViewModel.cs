namespace StudyBuddy.ViewModels;

public partial class LearningViewModel : BaseViewModel
{
	//initialize database service
	private DatabaseService _databaseService;

	[ObservableProperty]
	private string question;
	[ObservableProperty]
	private string answer;
	[ObservableProperty]
	private string categoryName;
	[ObservableProperty]
	private int completition;
	[ObservableProperty]
	private bool visibilityAnswer;
	[ObservableProperty]
	private bool visibilityCheck;

	//selected
	[ObservableProperty]
	public CategoryModel selectedCategory; 

	//lists
	[ObservableProperty]
	public List<CategoryModel> categories;
	[ObservableProperty]
	public List<FlashCardModel> flashcards;
	[ObservableProperty]
	public List<FlashCardModel> correctflashcardanswer;
	[ObservableProperty]
	public List<ProgressModel> progresses;

	public LearningViewModel()
	{
		_databaseService = new DatabaseService();

		Categories = new List<CategoryModel>();
		Flashcards = new List<FlashCardModel>();
		Correctflashcardanswer = new List<FlashCardModel>();

		GetAllCategories();
		VisibilityAnswer = false;
	}

	//clear strings lists .....
	private void ClearStrings()
	{
		Question = "";
		Answer = "";
		CategoryName = "";
		Flashcards.Clear();
		VisibilityAnswer = false;
		VisibilityCheck = false;
	}

	//get all categories from database
	private async void GetAllCategories()
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

	//get random flashcard from database where the category is the selected category
	//check if the question is already in the correctflashcardanswer list
	//if the question is already in the list, get a new random flashcard
	//else show the question
	[RelayCommand]
	private async Task GetRandomFlashCard()
	{
		ClearStrings();

		try
		{
			Flashcards = _databaseService.GetFlashCards();
			if(Flashcards.Count == Correctflashcardanswer.Count)
			{
				await Shell.Current.DisplayAlert("Random Flashcard", "You have answered all flash cards", "OK");

				//check if the progress is already in the database
				//Progresses = _databaseService.GetProgress(SelectedCategory.Id);

				//if(Progresses == null)
				//{
				//	var newprogress = new ProgressModel
				//	{
				//		Id = Progresses.Count + 1,
				//		CategoryId = SelectedCategory.Id,
				//		Completition = 1
				//	};

				//	_databaseService.AddProgress(newprogress);
				//}
				//else
				//{

				//	Completition = Progresses[0].Completition + 1;

				//	//if the progress is not in the database, add the progress
				//	_databaseService.UpdateProgress(new ProgressModel { CategoryId = SelectedCategory.Id, Completition = Completition });
				//}

				Correctflashcardanswer.Clear();
				await GetRandomFlashCard();
			}
			else
			{
				try
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
				catch(Exception ex)
				{
					await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
				}
			}
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	//add the answer to the list correctflashcardanswer
	[RelayCommand]
	private void CorrectAnswer()
	{
		//add the answer to the list correctflashcardanswer
		Correctflashcardanswer.Add(new FlashCardModel { Question = Question, Answer = Answer });
		VisibilityCheck = true;
	}

	//show the answer
	[RelayCommand]
	private void VisibleAnswer()
	{
		VisibilityAnswer = true;
	}
}
