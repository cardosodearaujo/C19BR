using Everaldo.Cardoso.Araujo.C19BR.Framework.Bases;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Notification;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Resources;

namespace Everaldo.Cardoso.Araujo.C19BR.Domain.Validations
{
    public class ValueValidation : BaseValidation<object>
    {
        public override void Validate(object objeto)
        {
            if (objeto == null)
            {
                Messages.Add(new Message(ReturnMessagesResources.MessageObjectNull));
            }
        }
    }
}
