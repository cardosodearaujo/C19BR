using System;
using System.ComponentModel;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    [Serializable]
    public abstract class BaseModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    
        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();
    }              
}
