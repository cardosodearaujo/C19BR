
using Everaldo.Cardoso.C19BR.Framework.Models;
using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Framework.ToolBox
{
    public static class StatesOfBrazil
    {
        public static List<States> getStatesOfBrazil()
        {
            return new List<States>
            {
                new States{ Code = 0, UF="AC", Name = "ACRE" },
                new States{ Code = 1, UF="AL",  Name = "ALAGOAS" },
                new States{ Code = 2, UF="AP",  Name = "AMAPÁ" },
                new States{ Code = 3, UF="AM",  Name = "AMAZONAS" },
                new States{ Code = 4, UF="BA",  Name = "BAHIA" },
                new States{ Code = 5, UF="CE",  Name = "CEARÁ" },
                new States{ Code = 6, UF="DF",  Name = "DISTRITO FEDERAL" },
                new States{ Code = 7, UF="ES",  Name = "ESPIRITO SANTO" },
                new States{ Code = 8, UF="MA",  Name = "MARANHÃO" },
                new States{ Code = 9, UF="GO", Name = "GOIÁS" },
                new States{ Code = 10, UF="MT", Name = "MATO GROSSO" },
                new States{ Code = 11, UF="MS", Name = "MATO GROSSO DO SUL" },
                new States{ Code = 12, UF="MG", Name = "MINAS GERAIS" },
                new States{ Code = 13, UF="PA", Name = "PARÁ" },
                new States{ Code = 14, UF="PB", Name = "PARAÍBA" },
                new States{ Code = 15, UF="PR", Name = "PARANÁ" },
                new States{ Code = 16, UF="PE", Name = "PERNANBUCO" },
                new States{ Code = 17, UF="PI", Name = "PIAUÍ" },
                new States{ Code = 18, UF="RJ", Name = "RIO DE JANEIRO" },
                new States{ Code = 19, UF="RN", Name = "RIO GRANDE DO NORTE" },
                new States{ Code = 20, UF="RS", Name = "RIO GRANDE DO SUL" },
                new States{ Code = 21, UF="RN", Name = "RONDÔNIA" },
                new States{ Code = 22, UF="RR", Name = "RORAIMA" },
                new States{ Code = 23, UF="SC", Name = "SANTA CATARINA" },
                new States{ Code = 24, UF="SP", Name = "SÃO PAULO" },
                new States{ Code = 25, UF="SE", Name = "SERGIPE" },
                new States{ Code = 26, UF="TO", Name = "TOCANTINS" }
            };
        }
    }
}
