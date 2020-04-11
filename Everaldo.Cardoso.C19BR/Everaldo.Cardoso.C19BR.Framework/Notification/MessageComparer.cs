using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Framework.Notification
{
    public class MessageComparer : IEqualityComparer<Message>
    {
        public bool Equals(Message x, Message y)
        {
            return x.Code.Equals(y.Code) &&
                x.Title.Equals(y.Title) &&
                x.Description.Equals(y.Description);
        }

        public int GetHashCode(Message obj)
        {
            return (obj.Code + obj.Title + obj.Description).GetHashCode();
        }
    }
}
