using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Objects.Brasil;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.HttpTransaction;
using Everaldo.Cardoso.C19BR.Framework.Notification;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Everaldo.Cardoso.C19BR.Domain.Services
{
    public class CasesBrasilService : BaseService
    {
        public async Task<ListOfCases> GetCasesFromStates()
        {
            try
            {
                HttpRequestResponse response = await new HttpRequest().Get(APILinksBase.Casos.GetValue().ToString() + "?is_last=True&place_type=state");
                if (response.Success)
                {
                    var cases = JsonConvert.DeserializeObject<ListOfCases>(response.HttpResult.Content.ReadAsStringAsync().Result);
                    if (cases != null && cases.count > 0)
                    {
                        return cases;
                    }
                }
                Messages.Add(new Message(response.Exception.Message));
                return null;
            }
            catch (Exception ex)
            {
                Messages.Add(new Message(ex.Message));
                return null;
            }            
        }

        public async Task<ListOfCases> GetCasesFromCity(States state)
        {
            try
            {
                HttpRequestResponse response = await new HttpRequest().Get(APILinksBase.Casos.GetValue().ToString() + "?page_size=10.000&is_last=True&state=" + state.GetValue().ToString());
                if (response.Success)
                {
                    var cases = JsonConvert.DeserializeObject<ListOfCases>(response.HttpResult.Content.ReadAsStringAsync().Result);
                    if (cases != null && cases.count > 0)
                    {
                        return cases;
                    }
                }
                Messages.Add(new Message(response.Exception.Message));
                return null;
            }
            catch (Exception ex)
            {
                Messages.Add(new Message(ex.Message));
                return null;
            }
        }

        public async Task<ListOfCases> GetTimeLineFromState(States state)
        {
            try
            {
                HttpRequestResponse response = await new HttpRequest().Get(APILinksBase.Casos.GetValue().ToString() + "?page_size=10.000&place_type=state&state=" + state.GetValue().ToString());
                if (response.Success)
                {
                    var cases = JsonConvert.DeserializeObject<ListOfCases>(response.HttpResult.Content.ReadAsStringAsync().Result);
                    if (cases != null && cases.count > 0)
                    {
                        return cases;
                    }
                }
                Messages.Add(new Message(response.Exception.Message));
                return null;
            }
            catch (Exception ex)
            {
                Messages.Add(new Message(ex.Message));
                return null;
            }
        }
    }
}
