using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Objects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.HttpTransaction;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Everaldo.Cardoso.C19BR.Framework.Notification;

namespace Everaldo.Cardoso.C19BR.Domain.Services
{
    public class CasesService : BaseService
    {
        public async Task<ListOfCases> GetCasesByStates()
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
    }
}
