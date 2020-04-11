using System;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumValue : Attribute
    {
        public object Value { get; private set; }

        public EnumValue(object value)
        {
            this.Value = value;
        }
    }
}
