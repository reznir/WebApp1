using System.Drawing;

namespace WebApp1.Models.Drd
{
    public class Postavy
    {
        public int Sudba { get; set; }

        public int Count { get; set; }

        public List<Postava> Postavas { get; set; } = new List<Postava>();

        public class Postava
        {
            public int ID { get; set; }
            public string Popis { get; set; }
            public int Iniciativa1 { get; private set; }
            public int Iniciativa2 { get; private set; }
            public string Color { get; set; }
            public int Ohrozeni { get; set; }

            public Postava(int id)
            {
                ID = id;
                Ohrozeni = 2;
                Color color = System.Drawing.Color.FromName(((Colors)(id%20)).ToString());
                Color = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                Popis = string.Concat("Postava ", ID.ToString());
                CountIniciativ();
            }

            public void CountIniciativ()
            {
                Random random = new Random();
                Iniciativa1 = random.Next(1, 7);
                Iniciativa2 = random.Next(1, 7);
            }
        }

        public enum Colors
        {
            Red = 0,
            Green = 1,
            Blue = 2,
            Yellow = 3,
            Black = 4,
            White = 5,
            Violet = 6,
            Pink = 7,
            Teal = 8,
            Tan = 9,
            Brown = 10,
            Navy = 11,
            Lime = 12,
            PaleTurquoise = 13,
            Magenta = 14,
            SkyBlue = 15,
            Silver = 16,
            Purple = 17,
            OliveDrab = 18,
            OrangeRed = 19,
        }
    }
}
