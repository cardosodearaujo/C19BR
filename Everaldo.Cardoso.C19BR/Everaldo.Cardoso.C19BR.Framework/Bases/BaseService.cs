using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.Notification;
using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    public class BaseService
    {
        public List<Message> Messages { get; set; }
        public EnumEnvironment Environment { get; set; }

        public BaseService()
        {
            this.Messages = new List<Message>();
        }        
    }
}
