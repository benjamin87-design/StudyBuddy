using StudyBuddy.Models;

namespace StudyBuddy.ViewModels;

public partial class CategoryViewModel : BaseViewModel
{
	private DatabaseService _databaseService;

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

		GetAllCategoriesFromDb();
	}

	//Get all categories from database
	public async void GetAllCategoriesFromDb()
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

	//Add category to database
	[RelayCommand]
	public async Task AddCategoryToDb()
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
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
		}
	}

	[RelayCommand]
	public async Task UpdateCategory(CategoryModel categoryToUpdate)
	{
		if(selectedCategory == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a category to update", "OK");
			return;
		}
		else
		{
			try
			{
				// Modify the properties of the employee object here...
				categoryToUpdate.CategoryName = CategoryName;
				categoryToUpdate.SubCategory = SubCategory;
				categoryToUpdate.Booklet = Booklet;

				_databaseService.UpdateCategory(categoryToUpdate);
				GetAllCategoriesFromDb();
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}

	[RelayCommand]
	public async Task DeleteCategory()
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
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
