using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.Model
{
    public enum PilotingTimeType
    {
        PIC,
        Dual
    }
    public class Flight
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string? AircraftType { get; set; }
        public string? AircraftIdent { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        public int Landings { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public PilotingTimeType PilotingTimeType { get; set; }

        public Flight()
        { }

        public Flight(int id, DateTime date, string? aircraftType, string? aircraftIdent, string? departure, string? arrival, int landings, DateTime departureTime, DateTime arrivalTime, TimeSpan totalTime, PilotingTimeType pilotingTimeType)
        {
            Id = id;
            this.date = date;
            AircraftType = aircraftType;
            AircraftIdent = aircraftIdent;
            Departure = departure;
            Arrival = arrival;
            Landings = landings;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TotalTime = totalTime;
            PilotingTimeType = pilotingTimeType;
        }
    }
}
