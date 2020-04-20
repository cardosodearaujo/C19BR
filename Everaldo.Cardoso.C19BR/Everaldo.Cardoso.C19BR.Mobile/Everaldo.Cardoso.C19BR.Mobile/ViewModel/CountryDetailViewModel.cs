using Everaldo.Cardoso.C19BR.Domain.Objects.World;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Microcharts;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class CountryDetailViewModel : BaseViewModel
    {
        public CountryDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        public Command _ListStates;
        public Command ListStates
        {
            get { return _ListStates; }
            set { SetProperty(ref _ListStates, value); }
        }


        private string _Location;
        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }


        private string _Population;
        public string Population
        {
            get { return _Population; }
            set { SetProperty(ref _Population, value); }
        }


        private string _Recovered;
        public string Recovered
        {
            get { return _Recovered; }
            set { SetProperty(ref _Recovered, value); }
        }


        private string _NumberDeaths;
        public string NumberDeaths
        {
            get { return _NumberDeaths; }
            set { SetProperty(ref _NumberDeaths, value); }
        }


        private string _NumberCases;
        public string NumberCases
        {
            get { return _NumberCases; }
            set { SetProperty(ref _NumberCases, value); }
        }


        private string _DeathsRate;
        public string DeathsRate
        {
            get { return _DeathsRate; }
            set { SetProperty(ref _DeathsRate, value); }
        }


        private string _Critical;
        public string Critical
        {
            get { return _Critical; }
            set { SetProperty(ref _Critical, value); }
        }


        private string _ConfirmedToday;
        public string ConfirmedToday
        {
            get { return _ConfirmedToday; }
            set { SetProperty(ref _ConfirmedToday, value); }
        }


        private string _DeathsToday;
        public string DeathsToday
        {
            get { return _DeathsToday; }
            set { SetProperty(ref _DeathsToday, value); }
        }


        private Chart _ChartDataCaseAccumulated;
        public Chart ChartDataCaseAccumulated
        {
            get { return _ChartDataCaseAccumulated; }
            set { SetProperty(ref _ChartDataCaseAccumulated, value); }
        }


        private Chart _ChartDataDeathsAccumulated;
        public Chart ChartDataDeathsAccumulated
        {
            get { return _ChartDataDeathsAccumulated; }
            set { SetProperty(ref _ChartDataDeathsAccumulated, value); }
        }        


        private Chart _ChartDataNewCases;
        public Chart ChartDataNewCases
        {
            get { return _ChartDataNewCases; }
            set { SetProperty(ref _ChartDataNewCases, value); }
        }


        private string _DateUpdate;
        public string DateUpdate
        {
            get { return _DateUpdate; }
            set { SetProperty(ref _DateUpdate, value); }
        }


        private Command _Refresh;
        public Command Refresh
        {
            get { return _Refresh; }
            set { SetProperty(ref _Refresh, value); }
        }


        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { SetProperty(ref _IsRefreshing, value); }
        }
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                Refresh = new Command(LoadData);
                await LoadDataCountry();
            }
            catch (Exception ex)
            {
                Dialog.Toast(ex.Message, TimeSpan.FromSeconds(5));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void LoadData()
        {
            IsRefreshing = true;
            try
            {
                await LoadDataCountry();
            }
            catch (Exception ex)
            {
                Dialog.Toast(ex.Message, TimeSpan.FromSeconds(5));
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task LoadDataCountry()
        {
            var service = new CasesWorldService();
            var cases = await service.GetCasesFromBrasil();

            if (cases != null && cases.data != null)
            {
                var data = cases.data;

                Location = "Brasil";
                Recovered = string.Format("{0:N0}", data.latest_data.recovered);
                NumberCases = string.Format("{0:N0}", data.latest_data.confirmed);
                NumberDeaths = string.Format("{0:N0}", data.latest_data.deaths);
                Critical = string.Format("{0:N0}", data.latest_data.critical);
                Population = string.Format("{0:N0}", data.population);
                ConfirmedToday = string.Format("{0:N0}", data.today.confirmed);
                DeathsToday = string.Format("{0:N0}", data.today.deaths);
                DeathsRate = string.Format("{0:N1}", (data.latest_data.calculated.death_rate)) + "%";
                DateUpdate = data.updated_at.ToString("dd/MM/yyyy");

                if (cases.data.timeline != null && cases.data.timeline.Count > 0)
                {
                    var timeline = (from line in cases.data.timeline
                                    where line.date >= DateTime.Now.AddDays(-15)  
                                    orderby line.date ascending
                                    select line).ToList();

                    if (timeline != null && timeline.Count > 0)
                    {
                        LoadChartCasesAccumulated(timeline);
                        LoadChartDeathsAccumulated(timeline);
                        LoadChartNewCases(timeline);
                    }
                }              
            }            
        }

        private void LoadChartCasesAccumulated(IList<Day> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.confirmed)
                    {
                        Label = day.date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.confirmed)
                    });
                    black = black ? false : true;
                }

                ChartDataCaseAccumulated = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 20,
                    BackgroundColor = SKColors.Transparent
                };
            }            
        }

        private void LoadChartDeathsAccumulated(IList<Day> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.deaths)
                    {
                        Label = day.date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.deaths)
                    });
                    black = black ? false : true;
                }
                
                ChartDataDeathsAccumulated = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 20,
                    BackgroundColor = SKColors.Transparent
                };
            }
        }

        private void LoadChartNewCases(IList<Day> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.new_confirmed)
                    {
                        Label = day.date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.new_confirmed),
                    });
                    black = black ? false : true;
                }

                ChartDataNewCases = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 20,
                    BackgroundColor = SKColors.Transparent
                };
            }
        }
        #endregion
    }
}
