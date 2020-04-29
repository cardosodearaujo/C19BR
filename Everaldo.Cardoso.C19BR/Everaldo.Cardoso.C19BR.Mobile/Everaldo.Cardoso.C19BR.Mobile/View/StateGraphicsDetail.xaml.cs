using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Objects.Brasil;
using Everaldo.Cardoso.C19BR.Domain.Services;
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

        public void Close(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}