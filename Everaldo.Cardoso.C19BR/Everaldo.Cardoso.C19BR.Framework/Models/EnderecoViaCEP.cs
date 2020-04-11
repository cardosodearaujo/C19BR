using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Everaldo.Cardoso.C19BR.Framework.Models
{
    public class EnderecoViaCEP : ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public object Clone()
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter f = new BinaryFormatter();
            f.Serialize(m, this);
            m.Seek(0, SeekOrigin.Begin);
            return f.Deserialize(m);
        }

        public string cep { get; set; }

        public string logradouro { get; set; }

        public string complemento { get; set; }

        public string bairro { get; set; }

        public string localidade { get; set; }

        public string uf { get; set; }

        public string unidade { get; set; }

        public string ibge { get; set; }

        public string gia { get; set; }
    }
}
