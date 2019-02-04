using Bartup.Contracts;
using BartUp.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bartup
{
    public class BartWebRequest
    {
        private static Dictionary<string, string> _bartStations;

        static BartWebRequest()
        {
            InitializeBartStations();
        }

        private async static void InitializeBartStations()
        {

            using (var stream = await FileSystem.OpenAppPackageFileAsync("BartStations.csv"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = await reader.ReadToEndAsync();
                    _bartStations = fileContents.Split('\n').Select(station => station.Split(','))
                               .ToDictionary(parsed => parsed[0], parsed => string.Concat(parsed.Skip(1)));
                }
            }
        }

        private static readonly HttpClient _webClient = new HttpClient();

        private const string _webRequestUri = @"http://api.bart.gov/api/etd.aspx?cmd=etd&orig={0}&key=MW9S-E7SL-26DU-VV8V&Json=y";
        public BartWebRequest()
        {
        }


        private TimeSpan GetMinutes(string input)
        {
            if (Int64.TryParse(input, out long minutes))
            {
                return TimeSpan.FromMinutes(minutes);
            }
            else
            {
                return TimeSpan.FromMinutes(0); // "leaving"
            }

        }

        public async Task<StationEtdInfo> GetBartEtdForStation(string stationCode)
        {
            var bartResponse = await GetBartResponseForStation(stationCode);

            var newStationEdtInfo = new StationEtdInfo()
            {
                Departures = bartResponse.Root.Station.First().Etd.Select(etd => 
                                                new EstimatedDeparture() {
                                                    DepartureEstimate = GetMinutes(etd.Estimate.First().Minutes),
                                                    FromStation = _bartStations[stationCode],
                                                    TowardsStation = etd.Destination
                                                }).ToList(),
                StationAbbreviation = stationCode,
                StationName = _bartStations[stationCode]
            };

            return newStationEdtInfo;
        }

        private async Task<BartResponse> GetBartResponseForStation(string stationCode)
        {
            var httpResponse = await _webClient.GetAsync(string.Format(_webRequestUri, stationCode));
            httpResponse.EnsureSuccessStatusCode();
            var rawString = await httpResponse.Content.ReadAsStringAsync();

            var jsonResponse = BartResponse.FromJson(rawString);
            return jsonResponse;
        }

       
    }
}
