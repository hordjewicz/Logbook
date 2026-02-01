using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogBook.Model;

namespace LogBook.ViewModel
{
    public class LogViewVM
    {
        private ObservableCollection<Flight> FlightInfo;
        public ObservableCollection<Flight> FlightInfoCollection
        {
            get { return FlightInfo; }
            set { this.FlightInfo = value; }
        }

        public LogViewVM()
        {
            FlightInfo = new ObservableCollection<Flight>();
            this.ReadFlightsFromDatabase();
        }

        public void ReadFlightsFromDatabase()
        {
            FlightInfo.Add(
                new Flight(1, 
                            new DateTime(2025, 04, 14), 
                            "FK9", 
                            "SP-SPAR", 
                            "EPWJ", 
                            "EPWJ", 
                            1, 
                            new DateTime(2025, 04, 14, 18, 30, 0), 
                            new DateTime(2025, 04, 14, 19, 45, 0), 
                            new TimeSpan(1, 45, 0), 
                            PilotingTimeType.Dual));

            FlightInfo.Add(
                new Flight(2,
                            new DateTime(2026, 04, 15),
                            "FK9",
                            "SP-SPAR",
                            "EPWJ",
                            "EPWJ",
                            1,
                            new DateTime(2025, 04, 15, 18, 20, 0),
                            new DateTime(2025, 04, 15, 19, 25, 0),
                            new TimeSpan(1, 15, 0),
                            PilotingTimeType.PIC));
        }
    }
}
