using System.ComponentModel.DataAnnotations;
using WebApp1.Models;

namespace WebApp1.Models
{
    public class StartEnd : ICipher
    {
        public string Encrypted { get; set; }
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public StartEnd()
        {
            Text = string.Empty;
            Encrypted = string.Empty;
            Id = 0;
        }
        public StartEnd(string text)
        {
            Text = text;
            Encrypt();
        }

        public string Encrypt()
        {
            try
            {
                var words = Text.Split(" ");
                foreach (var word in words)
                {
                    char[] letters = word.ToCharArray();
                    char[] encoded = new char[letters.Length];
                    for (int i = 0; i < letters.Length; i++)
                    {
                        if (letters[i] == ',' || letters[i] == '.' || letters[i] == '-' || letters[i] == '?' || letters[i] == '!' || letters[i] == ';')
                        { continue; }
                        if (i % 2 == 0)
                        {
                            encoded[i / 2] = letters[i];
                        }
                        else
                        {
                            encoded[letters.Length - (i / 2) - 1] = letters[i];
                        }
                    }
                    Encrypted = string.Concat(Encrypted, new string(encoded), " ");
                }
                return Encrypted;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
