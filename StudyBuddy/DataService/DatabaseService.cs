using Microsoft.Data.Sqlite;

namespace StudyBuddy.DataService
{
	public class DatabaseService
	{
		private string dbPath;

		public DatabaseService()
		{
			dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "StudyBuddy.db");
		}

		#region Category Methods

		//Add category to database
		public void AddCategory(Models.CategoryModel categories)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					INSERT INTO categories(
						categoryname,
						subcategory,
						booklet
					)
					VALUES(
						$categoryname,
						$subcategory,
						$booklet
					)
				";

				command.Parameters.AddWithValue("$categoryname", categories.CategoryName);
				command.Parameters.AddWithValue("$subcategory", categories.SubCategory);
				command.Parameters.AddWithValue("$booklet", categories.Booklet);

				command.ExecuteNonQuery();
			}
		}

		//Get all categories from database
		public List<Models.CategoryModel> GetCategories()
		{
			var categories = new List<Models.CategoryModel>();

			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					SELECT
						id,
						categoryname,
						subcategory,
						booklet
					FROM categories
				";

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var category = new Models.CategoryModel();
					category.Id = reader.GetInt32(0);
					category.CategoryName = reader.GetString(1);
					category.SubCategory = reader.GetString(2);
					category.Booklet = reader.GetString(3);

					categories.Add(category);
				}
			}

			return categories;
		}

		//Update category in database
		public void UpdateCategory(Models.CategoryModel categories)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					UPDATE categories
					SET
						categoryname = $categoryname,
						subcategory = $subcategory,
						booklet = $booklet
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$categoryname", categories.CategoryName);
				command.Parameters.AddWithValue("$subcategory", categories.SubCategory);
				command.Parameters.AddWithValue("$booklet", categories.Booklet);
				command.Parameters.AddWithValue("$id", categories.Id);

				command.ExecuteNonQuery();
			}
		}

		//Delete category from database
		public void DeleteCategory(Models.CategoryModel categories)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					DELETE FROM categories
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$id", categories.Id);

				command.ExecuteNonQuery();
			}
		}

		#endregion

		#region FlashCard Methods

		//Add flashcard to database
		public void AddFlashCard(Models.FlashCardModel flashcards)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					INSERT INTO flashcards(
						question,
						answer,
						categoryid
					)
					VALUES(
						$question,
						$answer,
						$categoryid
					)
				";

				command.Parameters.AddWithValue("$question", flashcards.Question);
				command.Parameters.AddWithValue("$answer", flashcards.Answer);
				command.Parameters.AddWithValue("$categoryid", flashcards.CategoryId);

				command.ExecuteNonQuery();
			}
		}

		//Get all flashcards from database
		public List<Models.FlashCardModel> GetFlashCards()
		{
			var flashcards = new List<Models.FlashCardModel>();

			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					SELECT
						id,
						question,
						answer,
						categoryid
					FROM flashcards
				";

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var flashcard = new Models.FlashCardModel();
					flashcard.Id = reader.GetInt32(0);
					flashcard.Question = reader.GetString(1);
					flashcard.Answer = reader.GetString(2);
					flashcard.CategoryId = reader.GetInt32(3);

					flashcards.Add(flashcard);
				}
			}

			return flashcards;
		}

		//Update flashcard in database
		public void UpdateFlashCard(Models.FlashCardModel flashcards)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					UPDATE flashcards
					SET
						question = $question,
						answer = $answer,
						categoryid = $categoryid
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$question", flashcards.Question);
				command.Parameters.AddWithValue("$answer", flashcards.Answer);
				command.Parameters.AddWithValue("$categoryid", flashcards.CategoryId);
				command.Parameters.AddWithValue("$id", flashcards.Id);

				command.ExecuteNonQuery();
			}
		}

		//Delete flashcard from database
		public void DeleteFlashCard(Models.FlashCardModel flashcards)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					DELETE FROM flashcards
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$id", flashcards.Id);

				command.ExecuteNonQuery();
			}
		}

		#endregion
	}
}
