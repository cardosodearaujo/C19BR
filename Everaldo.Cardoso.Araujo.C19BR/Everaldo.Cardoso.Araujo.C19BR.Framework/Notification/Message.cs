using System;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Notification
{
    public class Message: ICloneable
    {
        public Message()
        {
        }
        public Message(string Description)
        {
            this.Code = "200";
            this.Title = "Alerta";
            this.Description = Description;
        }        
        public Message(string Code, string Title, string Description)
        {
            this.Code = Code;
            this.Title = Title;
            this.Description = Description;
        }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return (Message)MemberwiseClone();
        }
    }
}
