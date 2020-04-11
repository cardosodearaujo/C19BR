using Everaldo.Cardoso.C19BR.Framework.HttpTransaction;
using Everaldo.Cardoso.C19BR.Framework.Models;

namespace Everaldo.Cardoso.C19BR.Framework.ToolBox
{
    public static class ViaCEPService
    {
        private const string URL = "http://viacep.com.br/ws/{0}/json/";
        public static EnderecoViaCEP SearchCEPByVIACEP(string CEP)
        {
            try
            {
                return new HttpRequest(null, false).Download<EnderecoViaCEP>(string.Format(URL, FormatCEP(CEP)));                
            }
            catch
            {
                return null;
            }
        }
        private static string FormatCEP(string CEP)
        {
            return CEP.Replace(".", string.Empty).Replace("-", string.Empty);
        }
    }
}
