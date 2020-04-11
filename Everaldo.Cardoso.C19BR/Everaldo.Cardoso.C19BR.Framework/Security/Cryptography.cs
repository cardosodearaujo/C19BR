using System;
using System.Linq;

namespace Everaldo.Cardoso.C19BR.Framework.Security
{
    public static class Cryptography
    {
        public static string Encrypt(string text)
        {
            if ((text != string.Empty))
            {
                text = new string(text.Reverse().ToArray());
                Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(text);
                return Convert.ToBase64String(bytes);
            }
            return null;
        }

        public static string Desencrypt(string text)
        {
            if ((text != string.Empty))
            {
                Byte[] bytes = Convert.FromBase64String(text);
                var data = System.Text.Encoding.ASCII.GetString(bytes);
                data = new string(data.Reverse().ToArray());
                return data;
            }
            return null;
        }

    }
}

