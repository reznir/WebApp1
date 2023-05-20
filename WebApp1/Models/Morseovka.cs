namespace WebApp1.Models
{
    public class Morseovka : ICipher
    {
        public string Encrypted { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Shuffle { get; set; }

        public Dictionary<char, List<int>> Key { get; }
        public List<string> Lower { get; }
        public List<string> Upper { get; }

        public Morseovka()
        {
            Key = new Dictionary<char, List<int>>()
            {
                { 'a', new List<int>() { 0, 1 } },
                { 'b', new List<int>() { 1, 0, 0, 0 } },
                { 'c', new List<int>() { 1, 0, 1, 0 } },
                { 'd', new List<int>() { 1, 0, 0 } },
                { 'e', new List<int>() { 0 } },
                { 'f', new List<int>() { 0, 0, 1, 0 } },
                { 'g', new List<int>() { 1, 1, 0 } },
                { 'h', new List<int>() { 0,0,0,0 } },
                { 'i', new List<int>() { 0,0 } },
                { 'j', new List<int>() { 0,1,1,1 } },
                { 'k', new List<int>() { 1,0,1 } },
                { 'l', new List<int>() { 0,1,0,0 } },
                { 'm', new List<int>() { 1,1 } },
                { 'n', new List<int>() { 1,0 } },
                { 'o', new List<int>() { 1,1,1 } },
                { 'p', new List<int>() { 0,1,1,0 } },
                { 'q', new List<int>() { 1,1,0,1 } },
                { 'r', new List<int>() { 0,1,0 } },
                { 's', new List<int>() { 0,0,0 } },
                { 't', new List<int>() { 1 } },
                { 'u', new List<int>() { 0,0,1 } },
                { 'v', new List<int>() { 0,0,0,1 } },
                { 'w', new List<int>() { 0,1,1 } },
                { 'x', new List<int>() { 1,0,0,1 } },
                { 'y', new List<int>() { 1,0,1,1 } },
                { 'z', new List<int>() { 1,1,0,0 } },
            };
            Lower = new List<string>() { "a", "c", "e", "m", "n", "o", "r", "s", "u", "v", "w", "x" };
            Upper = new List<string>() { "b", "d", "f", "h", "k", "l" };

            Text = string.Empty;
            Encrypted = string.Empty;
        }

        public string Encrypt()
        {
            char[] text = Text.ToLower().ToCharArray();
            Random random = new();
            foreach (char c in text)
            {
                if (!Key.ContainsKey(c))
                {
                    var a = Encrypted.Substring(Encrypted.Length - 1);
                    if (Encrypted.Substring(Encrypted.Length - 1) == "|")
                    { Encrypted = Encrypted.Substring(0, Encrypted.Length - 1); }
                    Encrypted = string.Concat(Encrypted, c, " ");
                    continue;
                }
                foreach (int kod in Key[c])
                {
                    if (kod == 0)
                    { Encrypted = string.Concat(Encrypted, Lower[Shuffle ? random.Next(0, Lower.Count) : 0]); }
                    else
                    { Encrypted = string.Concat(Encrypted, Upper[Shuffle ? random.Next(0, Upper.Count) : 0]); }
                }
                Encrypted = string.Concat(Encrypted, "|");
            }
            return Encrypted;
        }
    }
}
