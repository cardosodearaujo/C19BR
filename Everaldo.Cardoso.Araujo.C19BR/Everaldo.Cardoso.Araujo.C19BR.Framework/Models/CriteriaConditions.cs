using System;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Models
{
    public class CriteriaConditions
    {
        public CriteriaConditions()
        {
            Property = string.Empty;
            Value = new object();
            Type = null;
        }

        public CriteriaConditions(string Property, object Value)
        {
            this.Property = Property;
            this.Value = Value;
            this.Type = null;
        }

        public CriteriaConditions(string Property, object Value, Type Type)
        {
            this.Property = Property;
            this.Value = Value;
            this.Type = Type;
        }

        public string Property { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }
    }
}
