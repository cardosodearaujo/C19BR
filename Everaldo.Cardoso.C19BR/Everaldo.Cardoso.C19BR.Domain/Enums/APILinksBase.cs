using Everaldo.Cardoso.C19BR.Framework.Enums;

namespace Everaldo.Cardoso.C19BR.Domain.Enums
{
    public enum APILinksBase
    {
        [EnumValue("https://brasil.io/api/dataset/covid19/caso/data")]
        Casos,
        [EnumValue("https://brasil.io/api/dataset/covid19/boletim/data")]
        Boletins,
        [EnumValue(" https://corona-api.com/countries ")]
        CasosMundiais
    }
}
