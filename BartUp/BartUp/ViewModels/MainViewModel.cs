using BartUp.Contracts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Bartup;
using System.Linq;
using Xamarin.Essentials;
using System.IO;

namespace BartUp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<StationEtdInfo> _stationEtds;
        private BartWebRequest _bartWebRequest;

        private List<string> _stationsToMonitor = new List<string>();
        public MainViewModel(INavigationService navigationService)
        {
            StationEtds = new ObservableCollection<StationEtdInfo>();
            _bartWebRequest = new BartWebRequest();
            UserStationDataFile = Path.Combine(FileSystem.AppDataDirectory, "stationsToRemeber.csv");
            //AddStationToList("embr");
            //AddStationToList("balb");
            LoadStationsFromUserData();
            AddStation = new DelegateCommand<string>(AddStationToList);
            RefreshStations = new DelegateCommand(RefreshStationEtds);
        }

        private void LoadStationsFromUserData()
        {
            if (File.Exists(UserStationDataFile))
            {
                _stationsToMonitor = File.ReadAllText(UserStationDataFile).Split(',')
                               .ToList();
            }
        }

        private async void RefreshStationEtds()
        {
            _stationEtds.Clear();
            foreach (var station in _stationsToMonitor)
            {
                var etd = await _bartWebRequest.GetBartEtdForStation(station);
                _stationEtds.Add(etd);
            }
            LastUpdated = DateTime.Now;
        }

        private void AddStationToList(string sta)
        {
            _stationsToMonitor.Add(sta);

            var persistAsCsv = string.Concat(_stationsToMonitor.Select(station => station + ",")).Trim(',');
            File.WriteAllText(UserStationDataFile, persistAsCsv);

        }

        public DelegateCommand<string> AddStation { get; set; }

        public DelegateCommand RefreshStations { get; set; }

        private DateTime _lastUpdated;

        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }

        public ObservableCollection<StationEtdInfo> StationEtds
        {
            get { return _stationEtds; }
            set { SetProperty(ref _stationEtds, value); }
        }

        public string UserStationDataFile { get; private set; }
    }
}
