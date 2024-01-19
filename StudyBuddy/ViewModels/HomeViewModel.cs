namespace StudyBuddy.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
	//initialize database service
	private DatabaseService _databaseService;

	//lists
	[ObservableProperty]
	public List<CategoryModel> categories;
	[ObservableProperty]
	public List<FlashCardModel> flashCards;
	[ObservableProperty]
	public List<InfoCardModel> infoCards;

	public HomeViewModel()
	{
		_databaseService = new DatabaseService();
		Categories = new List<CategoryModel>();
		FlashCards = new List<FlashCardModel>();

		GetAllCategories();
		GetAllFlashCards();

		CreateInfoCardForEachCategory();
	}

	//get all categories from database
	private void GetAllCategories()
	{
		Categories = _databaseService.GetCategories();
	}

	//get all flash cards from database
	private void GetAllFlashCards()
	{
		FlashCards = _databaseService.GetFlashCards();
	}

	//create info card for each category
	public void CreateInfoCardForEachCategory()
	{
		
	}
}
