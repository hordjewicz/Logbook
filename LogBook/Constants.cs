
using SQLite;

public static class Constants
{
    public const string DatabaseFilename = "logbook.db3";
    public const string BackupDatabaseFilename = "backuplogbook.db3";

    public const SQLiteOpenFlags Flags =
    SQLiteOpenFlags.ReadWrite |
    SQLiteOpenFlags.Create |
    SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
    Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static string BackupDatabasePath =>
    Path.Combine(FileSystem.AppDataDirectory, BackupDatabasePath);
}
