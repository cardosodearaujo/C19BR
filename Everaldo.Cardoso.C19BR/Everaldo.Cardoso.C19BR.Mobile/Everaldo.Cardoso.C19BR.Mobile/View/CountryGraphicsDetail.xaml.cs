using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Objects.World;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Microcharts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace Everaldo.Cardoso.C19BR.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryGraphicsDetail : PopupPage
    {
        public CountryGraphicsDetail(ItemSearchListVO item)
        {
            InitializeComponent();
            LoadData(item);
        }

        private void LoadData(ItemSearchListVO item)
        {
            lblCountryName.Text = item.Name;
            lblConfirmedToday.Text = item.ConfirmedToday;
            lblDeathsToday.Text = item.DeathsToday;
            LoadDataCountry(item);
        }

        private async void LoadDataCountry(ItemSearchListVO item)
        {
            aciLoading.IsRunning = true;
            aciLoading.IsVisible = true;
            stlGeral.IsVisible = false;
            try
            {
                var service = new CasesWorldService();
                var cases = await service.GetCasesFromCountry(item.Code);
                if (cases.data.timeline != null && cases.data.timeline.Count > 0)
                {
                    var timeline = (from line in cases.data.timeline
                                    where line.date >= DateTime.Now.AddDays(-16)
                                    orderby line.date ascending
                                    select line).ToList();

                    if (timeline != null && timeline.Count > 0)
                    {
                        LoadChartCasesAccumulated(timeline);
                        LoadChartDeathsAccumulated(timeline);
                        LoadChartNewCases(timeline);
                        LoadChartNewDeaths(timeline);
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message, TimeSpan.FromSeconds(5));
            }
            finally
            {
                aciLoading.IsRunning = false;
                aciLoading.IsVisible = false;
                stlGeral.IsVisible = true;
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

                cvwChartDataCaseAccumulated.Chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 18,
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

                cvwChartDataDeathsAccumulated.Chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 18,
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

                cvwChartDataNewCases.Chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 18,
                    BackgroundColor = SKColors.Transparent
                };
            }
        }

        private void LoadChartNewDeaths(IList<Day> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.new_deaths)
                    {
                        Label = day.date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.new_deaths),
                    });
                    black = black ? false : true;
                }

                cvwChartDataNewDeaths.Chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 3,
                    PointMode = PointMode.Circle,
                    PointSize = 10,
                    LabelTextSize = 18,
                    BackgroundColor = SKColors.Transparent
                };
            }
        }

        public void Close(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}