using LogBook;
using LogBook.Model;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Runtime.Versioning;

namespace LogBook.View
{
    public partial class AddFlight : ContentPage
    {
        private readonly Database _database;

        public AddFlight(Database database)
        {
            InitializeComponent();
            _database = database;

            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            timePickerD.Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetTimeZone).TimeOfDay;
            timePickerA.Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetTimeZone).TimeOfDay;
            timePickerT.Time = timePickerD.Time - timePickerA.Time;

            AirportD.TextChanged += (sender, e) =>
            {             
                AirportD.Text = e.NewTextValue?.ToUpper();
            };

            AirportA.TextChanged += (sender, e) =>
            {                
                AirportA.Text = e.NewTextValue?.ToUpper();
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            // zapis do bazy danych przykładu
            Flight flight = new Flight
            {
                AircraftIdent = "SP-SPAR",
                PilotingTimeType = PilotingTimeType.PIC
            };

            await _database.SaveFlightAsync(flight);




            // odczyt ostatniego wpisu
            Flight lastFlight = await _database.GetLastFlightAsync();
            if (lastFlight != null)
            {
                //DisplayAlert("Ostatni lot", $"Id: {lastFlight.Id}, AircraftIdent: {lastFlight.AircraftIdent}", "OK");
            }
            else
            {
                //DisplayAlert("Ostatni lot", "Brak lotów w bazie danych.", "OK");
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            List<Flight> flights = await _database.GetFlightsAsync();

            foreach (Flight flight in flights)
            {
                Console.WriteLine($"Aircraft Ident: {flight.AircraftIdent}, Type of Piloting Time: {flight.PilotingTimeType}");
            }

            //CounterBtn.Text = $"Flight saved";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnSendBackupButtonClicked(object sender, EventArgs e)
        {
            await _database.SendBackupByEmailAsync("recipient@example.com");
            await DisplayAlert("Backup", "Kopia zapasowa została wysłana e-mailem.", "OK");
        }



        private void timePickerA_TimeSelected(object sender, TimeChangedEventArgs e)
        {
            TimeSpan difference = timePickerA.Time - timePickerD.Time;
            timePickerT.Time = difference;
        }

        private void timePickerD_TimeSelected(object sender, TimeChangedEventArgs e)
        {
            TimeSpan difference = timePickerA.Time - timePickerD.Time;
            timePickerT.Time = difference;
        }
    }

}
