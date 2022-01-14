using System;

namespace ZHD_Manager
{
    internal class TableFilterData
    {
        public DateTime DepartureDate = DateTime.Now.Date;
        public bool UseDepartureDate = false;

        public TimeSpan DepartureFromTime = DateTime.Now.TimeOfDay;
        public TimeSpan DepartureToTime = DateTime.Now.TimeOfDay;
        public bool UseDepartureTime = false;

        public string ArrivalStation = "";
        public bool UseArrivalStation = false;

        public string DepartureStation = "";
        public bool UseDepartureStation = false;

        public bool UseFreeOpenTrains = false;
        public bool UseFreeClosedTrains = false;
    }
}