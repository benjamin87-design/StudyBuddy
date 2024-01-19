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

		//Delete category from database where id is id
		public void DeleteCategory(int Id)
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

				command.Parameters.AddWithValue("$id", Id);

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
						categoryname,
						categoryid
					)
					VALUES(
						$question,
						$answer,
						$categoryname,
						$categoryid
					)
				";

				command.Parameters.AddWithValue("$question", flashcards.Question);
				command.Parameters.AddWithValue("$answer", flashcards.Answer);
				command.Parameters.AddWithValue("$categoryname", flashcards.CategoryName);
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
						categoryname,
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
					flashcard.CategoryName = reader.GetString(3);
					flashcard.CategoryId = reader.GetInt32(4);

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
						categoryname = $categoryname,
						categoryid = $categoryid
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$question", flashcards.Question);
				command.Parameters.AddWithValue("$answer", flashcards.Answer);
				command.Parameters.AddWithValue("$categoryname", flashcards.CategoryName);
				command.Parameters.AddWithValue("$categoryid", flashcards.CategoryId);
				command.Parameters.AddWithValue("$id", flashcards.Id);

				command.ExecuteNonQuery();
			}
		}

		//Delete flashcard from database
		public void DeleteFlashCard(int Id)
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

				command.Parameters.AddWithValue("$id", Id);

				command.ExecuteNonQuery();
			}
		}

		#endregion

		//Get random flashcard from database where categoryname is categoryname
		public List<Models.FlashCardModel> GetRandomFlashCard(string categoryname)
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
						categoryname,
						categoryid
					FROM flashcards
					WHERE categoryname = $categoryname
					ORDER BY RANDOM()
					LIMIT 1
				";

				command.Parameters.AddWithValue("$categoryname", categoryname);

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var flashcard = new Models.FlashCardModel();
					flashcard.Id = reader.GetInt32(0);
					flashcard.Question = reader.GetString(1);
					flashcard.Answer = reader.GetString(2);
					flashcard.CategoryName = reader.GetString(3);
					flashcard.CategoryId = reader.GetInt32(4);

					flashcards.Add(flashcard);
				}
			}
			return flashcards;
		}

		#region Completition

		//add progress to database
		public void AddProgress(Models.ProgressModel progress)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					INSERT INTO progress(
						categoryid,
						completition
					)
					VALUES(
						$categoryid,
						$completition
					)
				";

				command.Parameters.AddWithValue("$categoryname", progress.CategoryId);
				command.Parameters.AddWithValue("$subcategory", progress.Completition);

				command.ExecuteNonQuery();
			}
		}

		//get all progress from database
		public List<Models.ProgressModel> GetProgress(int categoryid)
		{
			var progresses = new List<Models.ProgressModel>();

			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					SELECT
						id,
						categoryid,
						completition
					FROM progress
					WHERE categoryid = $categoryid
					ORDER BY RANDOM()
					LIMIT 1
				";

				command.Parameters.AddWithValue("$categoryid", categoryid);

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var progress = new Models.ProgressModel();
					progress.Id = reader.GetInt32(0);
					progress.CategoryId = reader.GetInt32(1);
					progress.Completition = reader.GetInt32(2);

					progresses.Add(progress);
				}
			}
			return progresses;
		}

		//update progress in database
		public void UpdateProgress(ProgressModel progressModel)
		{
			using (var connection = new SqliteConnection($"Data Source={dbPath}"))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText =
				@"
					UPDATE progress
					SET
						categoryid = $categorid,
						completition = $completition
					WHERE id = $id
				";

				command.Parameters.AddWithValue("$categorid", progressModel.CategoryId);
				command.Parameters.AddWithValue("$completition", progressModel.Completition);
				command.Parameters.AddWithValue("$id", progressModel.Id);

				command.ExecuteNonQuery();
			}
		}

		#endregion
	}
}
