namespace LogBook.View;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private void NewDatabaseBtn_Clicked(object sender, EventArgs e)
    {
		Database.Instance.DeleteDatabaseFile();
        using var _ = Database.Instance.InitializeDatabaseAsync();
    }

    private void BackupDatabaseBtn_Clicked(object sender, EventArgs e)
    {
        Database.Instance.BackupDatabase();
    }
}