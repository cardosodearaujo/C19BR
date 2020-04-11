using Everaldo.Cardoso.Araujo.C19BR.Framework.Bases;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Notification;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Resources;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Specifications;
using System;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Validations
{
    public class ObjectValidation : BaseValidation<object>
    {
        public override void Validate(object objeto)
        {
            if (new ObjectNullSpecification().IsSatisfiedBy(objeto))
            {
                Messages.Add(new Message(ReturnMessagesResources.MessageObjectNull));
            }
            else if (typeof(string) == objeto.GetType())
            {
                if (string.IsNullOrEmpty(objeto.ToString()))
                {
                    Messages.Add(new Message(ReturnMessagesResources.MessageIdEmpty));
                }
            }
            else if (typeof(int) == objeto.GetType())
            {
                if (int.Parse(objeto.ToString()) <= 0)
                {
                    Messages.Add(new Message(ReturnMessagesResources.MessageIdMinorOrZero));
                }
            }
            else if (typeof(long) == objeto.GetType())
            {
                if (long.Parse(objeto.ToString()) <= 0)
                {
                    Messages.Add(new Message(ReturnMessagesResources.MessageIdMinorOrZero));
                }
            }
            else if (typeof(DateTime) == objeto.GetType())
            {
                if (!new DateValidSpecification().IsSatisfiedBy(DateTime.Parse(objeto.ToString())))
                {
                    Messages.Add(new Message(ReturnMessagesResources.MessageDateInvalid));
                }                
            }
        }
    }
}
