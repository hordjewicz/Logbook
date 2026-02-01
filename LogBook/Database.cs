using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogBook.Model;

namespace LogBook
{
    public sealed class Database
    {
        private SQLiteAsyncConnection _database;

        private static readonly Lazy<Database> _instance = new Lazy<Database>(() => new Database());
        public static Database Instance => _instance.Value;

        public SQLiteAsyncConnection database => _database;
        public Database()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            InitializeDatabaseAsync().Wait();
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                await _database.CreateTableAsync<Flight>();
                await _database.CreateTableAsync<Aircraft>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }


        // Metoda do usuwania pliku bazy danych
        public void DeleteDatabaseFile()
        {
            if (File.Exists(Constants.DatabasePath))
            {
                File.Delete(Constants.DatabasePath);
            }
        }

        public async Task<Flight> GetLastFlightAsync()
        {
            return await _database.Table<Flight>()
                            .OrderByDescending(f => f.Id)
                            .FirstOrDefaultAsync();
        }

        public void BackupDatabase()
        {
            if (File.Exists(Constants.DatabasePath))
            {
                File.Copy(Constants.DatabasePath, Constants.BackupDatabaseFilename, true);
            }
        }

        public async Task SendBackupByEmailAsync(string recipientEmail)
        {
            if (Email.Default.IsComposeSupported)
            {
                string subject = "Backup bazy danych";
                string body = "W załączniku znajduje się kopia zapasowa bazy danych.";
                string[] recipients = new[] { recipientEmail };

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    BodyFormat = EmailBodyFormat.PlainText,
                    To = new List<string>(recipients)
                };

                BackupDatabase();

                if (message.Attachments is not null)
                {
                    message.Attachments.Add(new EmailAttachment(Constants.BackupDatabaseFilename));
                }
                else
                {
                    message.Subject = "E-mail bez załącznika";
                }

                await Email.Default.ComposeAsync(message);
            }
        }

        // Metody dla tabeli Flight
        public Task<List<Flight>> GetFlightsAsync()
        {
            return _database.Table<Flight>().ToListAsync();
        }

        public Task<Flight> GetFlightAsync(int id)
        {
            return _database.Table<Flight>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFlightAsync(Flight flight)
        {
            if (flight.Id != 0)
            {
                return _database.UpdateAsync(flight);
            }
            else
            {
                return _database.InsertAsync(flight);
            }
        }

        public Task<int> DeleteFlightAsync(Flight flight)
        {
            return _database.DeleteAsync(flight);
        }

        public Task<int> DeleteFlightByIdAsync(int id)
        {
            return _database.DeleteAsync<Flight>(id);
        }

        public Task<int> DeleteAllFlightsAsync()
        {
            return _database.DeleteAllAsync<Flight>();
        }

        public Task<List<Flight>> SearchFlightsByAircraftIdentAsync(string aircraftIdent)
        {
            return _database.Table<Flight>()
                            .Where(u => u.AircraftIdent != null && u.AircraftIdent.Contains(aircraftIdent))
                            .ToListAsync();
        }

        public Task<Flight> SearchFlightByIdAsync(int id)
        {
            return _database.Table<Flight>()
                            .Where(u => u.Id == id)
                            .FirstOrDefaultAsync();
        }

        // Metody dla tabeli Aircraft
        public Task<List<Aircraft>> GetAircraftAsync()
        {
            return _database.Table<Aircraft>().ToListAsync();
        }

        public Task<Aircraft> GetAircraftAsync(int id)
        {
            return _database.Table<Aircraft>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAircraftAsync(Aircraft aircraft)
        {
            if (aircraft.Id != 0)
            {
                return _database.UpdateAsync(aircraft);
            }
            else
            {
                return _database.InsertAsync(aircraft);
            }
        }

        public Task<int> DeleteAircraftAsync(Aircraft aircraft)
        {
            return _database.DeleteAsync(aircraft);
        }

        public Task<int> DeleteAircraftByIdAsync(int id)
        {
            return _database.DeleteAsync<Aircraft>(id);
        }

        public Task<int> DeleteAllAircraftsAsync()
        {
            return _database.DeleteAllAsync<Aircraft>();
        }

        public Task<Aircraft> SearchAircraftByIdAsync(int id)
        {
            return _database.Table<Aircraft>()
                            .Where(u => u.Id == id)
                            .FirstOrDefaultAsync();
        }

    }
}
