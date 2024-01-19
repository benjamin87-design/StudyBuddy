using Microsoft.Data.Sqlite;

namespace StudyBuddy;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		InitializeDatabase();

		MainPage = new AppShell();
	}

	private void InitializeDatabase()
	{
		SQLitePCL.Batteries_V2.Init();

		var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "StudyBuddy.db");

		using (var connection = new SqliteConnection($"Data Source={dbPath}"))
		{
			connection.Open();

			var command = connection.CreateCommand();
			command.CommandText =
			@"
                CREATE TABLE IF NOT EXISTS categories(
                    id INTEGER PRIMARY KEY,
                    categoryname TEXT NOT NULL,
                    subcategory TEXT NOT NULL,
                    booklet TEXT NOT NULL
                )
            ";

			command.ExecuteNonQuery();

			command.CommandText =
			@"
                CREATE TABLE IF NOT EXISTS flashcards(
                    id INTEGER PRIMARY KEY,
                    question TEXT NOT NULL,
                    answer TEXT NOT NULL,
					categoryname TEXT NOT NULL,
                    categoryid INTEGER NOT NULL,
                    FOREIGN KEY (categoryid) REFERENCES categories (id)
                )
            ";

			command.ExecuteNonQuery();

			command.CommandText =
			@"
                CREATE TABLE IF NOT EXISTS progress(
                    id INTEGER PRIMARY KEY,
                    completition INTEGER NOT NULL,
					categoryid INTEGER NOT NULL
                )
            ";

			command.ExecuteNonQuery();

			command.CommandText =
			@"
                CREATE TABLE IF NOT EXISTS infocard(
                    id INTEGER PRIMARY KEY,
					title TEXT NOT NULL,
                    cardnumber INTEGER NOT NULL,
                    numberofcompletition INTEGER NOT NULL,
					starrating BOOL NOT NULL
                )
            ";

			command.ExecuteNonQuery();
		}
	}
}
