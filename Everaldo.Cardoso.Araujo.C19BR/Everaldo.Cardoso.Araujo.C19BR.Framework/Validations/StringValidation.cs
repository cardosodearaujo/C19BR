using Everaldo.Cardoso.Araujo.C19BR.Framework.Bases;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Notification;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Resources;

namespace Everaldo.Cardoso.Araujo.C19BR.Domain.Validations
{
    public class StringValidation : BaseValidation<string>
    {
        public override void Validate(string objeto)
        {
            if (objeto == null)
            {
                Messages.Add(new Message(ReturnMessagesResources.MessageObjectNull));
            }
            else if (string.IsNullOrEmpty(objeto))
            {
                Messages.Add(new Message(ReturnMessagesResources.MessageObjectEmpty));
            }
        }
    }
}
