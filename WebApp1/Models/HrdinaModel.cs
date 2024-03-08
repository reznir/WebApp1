using WebApp1.Models.Database;

namespace WebApp1.Models
{
    public class HrdinaModel
    {
        public Hrdina Hrdina { get; set; }

        /// <summary>
        /// Obsahuje seznam ID Povolani a jejich levely pro hrdinu
        /// List ze spojovaci tabulky filtrovany pro hrdinu.
        /// </summary>
        public List<HrdinaPovolani> HrdinovaPovolani { get; set; }

        /// <summary>
        /// Obsahuje senzma povolani, ktere dany hrdina ovlada
        /// </summary>
        public List<Povolani> Povolani { get; set; }

        /// <summary>
        /// Seznam schonosti hrdiny
        /// </summary>
        public List<Schopnost> Schopnosti { get; set; }

        /// <summary>
        /// Seznam ID Povolani, ktere se muze hrdina naucit
        /// </summary>
        /// <param name="hrdinovaPovolani"></param>
        /// <returns></returns>
        public List<int> PossiblePovolani(List<HrdinaPovolani> hrdinovaPovolani = null)
        {
            if (hrdinovaPovolani == null)
            { hrdinovaPovolani = HrdinovaPovolani; }

            List<int> moznaPovolaniIds = new();

            //zakladni povolani
            for (int i = 1; i < 6; i++)
            {
                if (!hrdinovaPovolani.Any(p => p.PovolaniId == i))
                { moznaPovolaniIds.Add(i); }
            }           

            //pokrocila povolani
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Bojovnik, (int)Povolanis.Lovec) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Hranicar))
            { moznaPovolaniIds.Add((int) Povolanis.Hranicar); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int) Povolanis.Lovec, (int) Povolanis.Kejklir) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int) Povolanis.Zved))
            { moznaPovolaniIds.Add((int)Povolanis.Zved); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Kejklir, (int)Povolanis.Mastickar) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Mag))
            { moznaPovolaniIds.Add((int)Povolanis.Mag); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Mastickar, (int)Povolanis.Zarikavac) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Alchymista))
            { moznaPovolaniIds.Add((int)Povolanis.Alchymista); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Bojovnik, (int)Povolanis.Kejklir) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Valecník))
            { moznaPovolaniIds.Add((int)Povolanis.Valecník); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Bojovnik, (int)Povolanis.Mastickar) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Vedmak))
            { moznaPovolaniIds.Add((int)Povolanis.Vedmak); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Bojovnik, (int)Povolanis.Zarikavac) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Carodej))
            { moznaPovolaniIds.Add((int)Povolanis.Carodej); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Lovec, (int)Povolanis.Mastickar) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Saman))
            { moznaPovolaniIds.Add((int)Povolanis.Saman); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Lovec, (int)Povolanis.Zarikavac) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Druid))
            { moznaPovolaniIds.Add((int)Povolanis.Druid); }
            if (TestPovolaniMozne(hrdinovaPovolani, (int)Povolanis.Mastickar, (int)Povolanis.Kejklir) && !hrdinovaPovolani.Any(p => p.PovolaniId == (int)Povolanis.Lupic))
            { moznaPovolaniIds.Add((int)Povolanis.Lupic); }

            return moznaPovolaniIds;
        }

        private bool TestPovolaniMozne(List<HrdinaPovolani> hrdinovaPovolani, int zakladniPov1, int zakladniPov2)
            => hrdinovaPovolani.FirstOrDefault(p => p.PovolaniId == zakladniPov1) is HrdinaPovolani zaklPov1
            && hrdinovaPovolani.FirstOrDefault(p => p.PovolaniId == zakladniPov2) is HrdinaPovolani zaklPov2
            && zaklPov1.Level + zaklPov2.Level >= 6;
            
        
    }

    public enum Povolanis
    {
        Bojovnik = 1,
        Lovec = 2,
        Kejklir = 3,
        Mastickar = 4,
        Zarikavac = 5,
        Mag = 6,
        Vedmak = 7,
        Saman = 8,
        Hranicar = 9,
        Valecník = 10,
        Druid = 11,
        Lupic = 12,
        Zved = 13,
        Alchymista = 14,
        Carodej = 15,
    }
}
