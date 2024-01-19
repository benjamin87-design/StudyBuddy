namespace StudyBuddy.ViewModels;

public partial class ManageCardsViewModel : BaseViewModel
{
	private DatabaseService _databaseService;

	[ObservableProperty]
	public int id;
	[ObservableProperty]
	public int categoryId;
	[ObservableProperty]
	public string categoryName;
	[ObservableProperty]
	public string question;
	[ObservableProperty]
	public string answer;

	[ObservableProperty]
	public CategoryModel selectedCategory;
	[ObservableProperty]
	public FlashCardModel selectedFlashCard;

	[ObservableProperty]
	public List<CategoryModel> categories;
	[ObservableProperty]
	public List<FlashCardModel> flashcards;

	public ManageCardsViewModel()
	{
		_databaseService = new DatabaseService();

		Categories = new List<CategoryModel>();
		Flashcards = new List<FlashCardModel>();

		GetAllCategoriesFromDb();
		GetAllFlashCardsFromDb();
	}

	//Get all categories from database
	private async void GetAllCategoriesFromDb()
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

	//get all flashcards from database
	private async void GetAllFlashCardsFromDb()
	{
		Flashcards.Clear();

		try
		{
			Flashcards = _databaseService.GetFlashCards();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	//clear strings and lists ....
	private void ClearStrings()
	{
		Id = 0;
		Question = "";
		Answer = "";
		CategoryName = "";
		CategoryId = 0;
	}

	//Add flashcard to database
	[RelayCommand]
	private async Task AddFlashCardToDb()
	{
		try
		{
			var newFlashCard = new FlashCardModel
			{
				Question = Question,
				Answer = Answer,
				CategoryName = SelectedCategory.CategoryName,
				CategoryId = SelectedCategory.Id
			};

			_databaseService.AddFlashCard(newFlashCard);

			GetAllFlashCardsFromDb();
			ClearStrings();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	//update flashcard in database
	[RelayCommand]
	private async Task UpdateFlashCard(FlashCardModel flashcardToUpdate)
	{
		if (SelectedFlashCard == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a flashcard to update", "OK");
			return;
		}
		else
		{
			flashcardToUpdate = SelectedFlashCard;
			try
			{
				flashcardToUpdate.Question = Question;
				flashcardToUpdate.Answer = Answer;
				flashcardToUpdate.CategoryName = SelectedCategory.CategoryName;
				flashcardToUpdate.CategoryId = SelectedCategory.Id;

				_databaseService.UpdateFlashCard(flashcardToUpdate);

				GetAllFlashCardsFromDb();
				ClearStrings();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}

	//delete flashcard from database
	[RelayCommand]
	private async Task DeleteFlashCard()
	{
		if (SelectedFlashCard == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a flashcard to delete", "OK");
			return;
		}
		else
		{
			try
			{
				_databaseService.DeleteFlashCard(SelectedFlashCard.Id);

				GetAllFlashCardsFromDb();
				ClearStrings();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}

}
