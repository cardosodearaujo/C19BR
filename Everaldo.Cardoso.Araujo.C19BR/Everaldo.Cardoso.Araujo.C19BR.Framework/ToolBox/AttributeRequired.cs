using System;
using System.ComponentModel.DataAnnotations;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.ToolBox
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AttributeRequired : RequiredAttribute
    {
        public bool IgnoreSubContext { get; set; }
        public string MainContext { get; set; }        

        public AttributeRequired()
        {
            IgnoreSubContext = false;
            MainContext = string.Empty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (RequiresValidationContext)
            {
                if (validationContext.ObjectInstance.GetType().Name != MainContext && IgnoreSubContext)
                {
                    if (validationContext.ObjectType != null)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("O " + validationContext.DisplayName + " é obrigatório.");
                    }
                }
            }
            
            return base.IsValid(value, validationContext);
        }
    }
}
