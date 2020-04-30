using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Objects.Brasil;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.ToolBox;
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
    public partial class StateGraphicsDetail : PopupPage
    {
        public StateGraphicsDetail(States state)
        {
            InitializeComponent();
            LoadData(state);
        }

        private async void LoadData(States state)
        {
            aciLoading.IsRunning = true;
            aciLoading.IsVisible = true;
            stlGeral.IsVisible = false;
            try
            {                
                var service = new CasesBrasilService();
                var cases = await service.GetTimeLineFromState(state);
                if (cases != null)
                {
                    lblStateName.Text = StatesOfBrazil.getStatesOfBrazil().Where(F => F.UF == state.GetValue().ToString()).FirstOrDefault().Name;
                                      
                    var timeline = (from line in cases.results
                                    where line.date >= DateTime.Now.AddDays(-16)
                                    && line.date < DateTime.Now.Date
                                    orderby line.date ascending
                                    select line).ToList();

                    LoadChartCasesAccumulated(timeline);
                    LoadChartDeathsAccumulated(timeline);

                    var timelineNotAccumulated = ConvertToNotAccumulated(cases.results);

                    LoadChartNewCases(timelineNotAccumulated);
                    LoadChartNewDeaths(timelineNotAccumulated);
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

        private List<CasesNotAccumulatedStateVO> ConvertToNotAccumulated(IList<Case> timeline)
        {
            timeline = (from F in timeline
                        where F.date >= DateTime.Now.AddDays(-17)
                        && F.date < DateTime.Now.Date
                        orderby F.date descending
                        select F).ToList();

            var casesNotAccumulatedState = new List<CasesNotAccumulatedStateVO>();

            var today = timeline.Where(Y => Y.date == timeline.Max(Z => Z.date)).FirstOrDefault();

            ConvertToNotAccumulatedRecursively(timeline, ref casesNotAccumulatedState, today);

            casesNotAccumulatedState = (from F in casesNotAccumulatedState
                                        where F.Date >= DateTime.Now.AddDays(-16)
                                        && F.Date < DateTime.Now.Date
                                        orderby F.Date ascending
                                        select F).ToList();

            return casesNotAccumulatedState;
        }

        private void ConvertToNotAccumulatedRecursively(IList<Case> timeline, ref List<CasesNotAccumulatedStateVO> casesNotAccumulatedState , Case today)
        {
            if (today != null)
            {
                var currentList = timeline.Where(X => X.date < today.date).ToList();

                if (currentList != null && currentList.Any())
                {
                    var yesterday = timeline.Where(W => W.date == currentList.Max(Z => Z.date)).FirstOrDefault();

                    if (yesterday != null)
                    {
                        casesNotAccumulatedState.Add(new CasesNotAccumulatedStateVO
                        {
                            Date = today.date,
                            Confirmed = today.confirmed - yesterday.confirmed,
                            Deaths = today.deaths - yesterday.deaths
                        });

                        ConvertToNotAccumulatedRecursively(timeline, ref casesNotAccumulatedState, yesterday);
                    }
                }                
            }            
        }

        private void LoadChartCasesAccumulated(IList<Case> timeline)
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

        private void LoadChartDeathsAccumulated(IList<Case> timeline)
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

        private void LoadChartNewCases(IList<CasesNotAccumulatedStateVO> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.Confirmed)
                    {
                        Label = day.Date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.Confirmed),
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

        private void LoadChartNewDeaths(IList<CasesNotAccumulatedStateVO> timeline)
        {
            if (timeline != null)
            {
                var entries = new List<Entry>();
                var black = false;

                foreach (var day in timeline)
                {
                    entries.Add(new Entry((float)day.Deaths)
                    {
                        Label = day.Date.ToString("dd"),
                        Color = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        TextColor = black ? SKColors.Black : SKColor.Parse("#ba6b6c"),
                        ValueLabel = string.Format("{0:N0}", day.Deaths),
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