using System;
using System.Collections.Generic;
using System.Text;

namespace BartUp.Contracts
{

    public class EstimatedDeparture
    {
        public TimeSpan DepartureEstimate { get; set; }

        public string FromStation { get; set; }

        public string TowardsStation { get; set; }
    }
}
