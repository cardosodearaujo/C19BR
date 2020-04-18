using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Objects.World;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.HttpTransaction;
using Everaldo.Cardoso.C19BR.Framework.Notification;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Everaldo.Cardoso.C19BR.Domain.Services
{
    public class CasesWorldService : BaseService
    {
        public async Task<ListOfCases> GetCasesFromWorld()
        {
            try
            {
                HttpRequestResponse response = await new HttpRequest().Get(APILinksBase.CasosMundiais.GetValue().ToString());
                if (response.Success)
                {
                    var cases = JsonConvert.DeserializeObject<ListOfCases>(response.HttpResult.Content.ReadAsStringAsync().Result);
                    if (cases != null && cases.data.Count > 0)
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

        public async Task<Header> GetCasesFromBrasil()
        {
            try
            {
                HttpRequestResponse response = await new HttpRequest().Get(APILinksBase.CasosMundiais.GetValue().ToString() + "/BR");
                if (response.Success)
                {
                    var cases = JsonConvert.DeserializeObject<Header>(response.HttpResult.Content.ReadAsStringAsync().Result);
                    if (cases != null && cases.data != null)
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
