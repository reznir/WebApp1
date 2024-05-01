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
                Popis = string.Concat("Postava ", ID.ToString());
                CountIniciativa();
            }

            public void CountIniciativa()
            {
                Random random = new Random();
                Iniciativa1 = random.Next(1,7);
                Iniciativa2 = random.Next(1,7);
            }
        }
    }
}
