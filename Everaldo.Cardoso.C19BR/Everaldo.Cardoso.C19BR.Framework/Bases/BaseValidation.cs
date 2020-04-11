using Everaldo.Cardoso.C19BR.Framework.Notification;
using Everaldo.Cardoso.C19BR.Framework.Resources;
using System.Collections.Generic;
using System.Reflection;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    public abstract class BaseValidation<T>
    {
        public List<Message> Messages { get; set; }

        public BaseValidation()
        {
            Messages = new List<Message>();
        }

        protected void AddMessage(string code, string title, string description)
        {
            Messages.Add(new Message(code, title, description));
        }

        protected void AddMessage(string description)
        {
            Messages.Add(new Message(description));
        }

        protected void AddError(string description)
        {
            Messages.Add(new Message(ReturnStatusResources.CodeMessageError, ReturnMessagesResources.MessageError, description));
        }

        protected void AddAlert(string description)
        {
            Messages.Add(new Message(ReturnStatusResources.CodeMessageAlert, ReturnMessagesResources.MessageAlert, description));
        }

        protected void AddSucess(string description)
        {
            Messages.Add(new Message(ReturnStatusResources.CodeMessageSucess, ReturnMessagesResources.MessageSucess, description));
        }

        public abstract void Validate(T objeto);

        protected virtual void SetLog(string log)
        {
            Messages.Add(new Message(log));
        }

        protected void ValidateDefaultParameters(T objeto)
        {
            foreach (var prop in objeto.GetType().GetProperties())
            {
                switch (prop.Name)
                {
                    case ("Id_Vendedor"):
                        if (!prop.PropertyType.IsGenericType)
                        {
                            if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                            {
                                SetLog("O vendedor é obrigatório!");
                            }
                            else
                            {
                                var propAdm = prop.GetValue(objeto);
                                var propAdmId = propAdm.GetType().GetProperty("Id_Vendedor", BindingFlags.Public | BindingFlags.Instance);
                                if (propAdmId.GetValue(propAdm) == null || propAdmId.GetValue(propAdm).ToString().Equals(string.Empty))
                                {
                                    SetLog("O vendedor é obrigatório!");
                                }
                            }
                        }
                        break;
                    case ("Id_Administradora"):
                        if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                        {
                            SetLog("A administradora é obrigatória!");
                        }
                        else
                        {
                            var propAdm = prop.GetValue(objeto);
                            var propAdmId = propAdm.GetType().GetProperty("Id_Administradora", BindingFlags.Public | BindingFlags.Instance);
                            if (propAdmId.GetValue(propAdm) == null || propAdmId.GetValue(propAdm).ToString().Equals(string.Empty))
                            {
                                SetLog("A administradora é obrigatória!");
                            }
                        }
                        break;
                    case ("Id_Usuario_Inc"):
                        if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                        {
                            SetLog("O usuario que incluiu o registro é obrigatório!");
                        }
                        break;
                    case ("Id_Usuario_Alt"):
                        if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                        {
                            SetLog("O usuario que alterou o registro é obrigatório!");
                        }
                        break;
                    case ("Dt_Inc"):
                        if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                        {
                            SetLog("A data de inclusão é obrigatória!");
                        }
                        break;
                    case ("Dt_Alt"):
                        if (prop.GetValue(objeto) == null || prop.GetValue(objeto).ToString().Equals(string.Empty))
                        {
                            SetLog("A data de alteração é obrigatória!");
                        }
                        break;
                }
            }
        }
    }
}
