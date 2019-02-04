using System;
using System.Collections.Generic;
using System.Text;

namespace BartUp.Contracts
{
    public class StationEtdInfo
    {
        public string StationName { get; set; }

        public string StationAbbreviation { get; set; }

        public List<EstimatedDeparture> Departures { get; set; }

        public StationEtdInfo()
        {
            Departures = new List<EstimatedDeparture>();
        }
    }
}
