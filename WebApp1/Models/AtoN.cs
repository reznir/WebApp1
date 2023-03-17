
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class AtoN : ICipher
    {
        public int Id { get; set; }

        public string Key { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public string Encrypted { get; set; }

        public AtoN()
        {
            Key = string.Empty;
            Text = string.Empty;
            Encrypted = string.Empty;
        }
        [ActivatorUtilitiesConstructor]
        public AtoN(string text, string key)
        {
            Text = text;
            Key = key;
            Encrypt();
        }

        public string Encrypt()
        {
            try
            {
                string text = Text.ToLower();
                int keyLength = Key.Length;
                int keyPointer = 0;
                char[] keyChars = Key.ToCharArray();
                foreach (char letter in text.ToCharArray())
                {
                    if (((short)letter) < 97 || ((short)letter) > 122)
                    {
                        Encrypted = string.Concat(Encrypted, letter);
                        continue;
                    }
                    int a = ((Convert.ToInt16(letter) + Convert.ToInt16(keyChars[keyPointer]) - 2 * 97) % 26) + 97;
                    Encrypted = string.Concat(Encrypted, Convert.ToChar(((Convert.ToInt16(letter) + Convert.ToInt16(keyChars[keyPointer]) - 2 * 97) % 26) + 97));
                    keyPointer = ++keyPointer % keyLength;
                }
                return Encrypted;
            }
            catch 
            { return string.Empty; }
        }
    }
}
