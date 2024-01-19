using StudyBuddy.Models;

namespace StudyBuddy.ViewModels;

public partial class CategoryViewModel : BaseViewModel
{
	private DatabaseService _databaseService;

	[ObservableProperty]
	public int id;
	[ObservableProperty]
	public string categoryName;
	[ObservableProperty]
	public string subCategory;
	[ObservableProperty]
	public string booklet;

	[ObservableProperty]
	public CategoryModel selectedCategory;

	[ObservableProperty]
	public List<CategoryModel> categories; 

	public CategoryViewModel()
	{
		_databaseService = new DatabaseService();
		Categories = new List<CategoryModel>();

		GetAllCategoriesFromDb();
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

	//clear strings lists.....
	private void ClearStrings()
	{
		Id = 0;
		CategoryName = "";
		SubCategory = "";
		Booklet = "";
	}

	//Add category to database
	[RelayCommand]
	private async Task AddCategoryToDb()
	{
		try
		{
			var newCategory = new CategoryModel
			{
				CategoryName = CategoryName,
				SubCategory = SubCategory,
				Booklet = Booklet
			};

			_databaseService.AddCategory(newCategory);

			GetAllCategoriesFromDb();
			ClearStrings();
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	//Update selected category
	[RelayCommand]
	private async Task UpdateCategory(CategoryModel categoryToUpdate)
	{
		if(SelectedCategory == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a category to update", "OK");
			return;
		}
		else
		{
			//pass selectedCategory to categoryToUpdate
			categoryToUpdate = SelectedCategory;

			try
			{
				// Modify the properties of the employee object here...
				categoryToUpdate.CategoryName = CategoryName;
				categoryToUpdate.SubCategory = SubCategory;
				categoryToUpdate.Booklet = Booklet;

				_databaseService.UpdateCategory(categoryToUpdate);
				GetAllCategoriesFromDb();
				ClearStrings();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}

	//Delete selected category
	[RelayCommand]
	private async Task DeleteCategory()
	{
		if(selectedCategory == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a category to delete", "OK");
			return;
		}
		else
		{
			try
			{
				_databaseService.DeleteCategory(SelectedCategory.Id);
				GetAllCategoriesFromDb();
				ClearStrings();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
