using System.Collections.Generic;
using System.Linq;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Notification
{
    public static class MessageErros
    {
        public static bool HasErrors(this List<Message> messages)
        {
            return messages != null && messages.Count() > 0 ? true : false;
        }

        public static void RemoveDuplicateMessages(this List<Message> messages)
        {
            var newErrorList = (from i in messages
                                select i).Distinct(new MessageComparer()).ToList();

            messages.Clear();
            messages.AddRange(newErrorList);
        }
    }
}
