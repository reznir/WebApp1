using WebApp1.Models;

namespace WebApp1.Models
{
    public class StartEnd : ICipher
    {
        public string Encrypted { get; set; }
        public int Id { get; set; }
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
            var words = Text.Split(" ");
            foreach (var word in words)
            {
                char[] letters = word.ToCharArray();
                for (int i = 1; i <= letters.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        letters[letters.Length - (i / 2)] = letters[i -1];
                    }
                    else
                    {
                        letters[i / 2] = letters[i - 1];
                    }
                }
                Encrypted = string.Concat(Encrypted, new string(letters), " ");                
            }
            return Encrypted;
        }
    }
}
