namespace WebApp1.Models
{
    public class LiturgickyRok
    {
        /// <summary>
        /// Datum velikonocni nedele
        /// </summary>
        public DateTime Velikonoce { get; set; }
        /// <summary>
        /// Datum popelecni stredy
        /// </summary>
        public DateTime PopelecniStreda { get; set; }
        /// <summary>
        /// Datum prvni adventni nedele
        /// </summary>
        public DateTime PrvniAdventni { get; set; }
        /// <summary>
        /// Cely nazev dne ve formatu "dayOfWeek NazevDne".
        /// Pokud neni svatek na cely tyden nezacina NazevDne cislem.
        /// </summary>
        public string NazevDne { get; set; }
        /// <summary>
        /// Nazev tydne nebo svatku bez poradoveho cisla dne v tydnu na rozdil od <see cref="NazevDne"/>
        /// </summary>
        public string TypSvatku { get; set; }

        public LiturgickyRok(DateTime datum)
        {
            PrvniAdventni = DatumPrvniAdventni(datum);
            if (datum > PrvniAdventni)
            {
                Velikonoce = DatumVelikonoc(datum.Year + 1);
            }
            else
            {
                Velikonoce = DatumVelikonoc(datum.Year);
            }
            PopelecniStreda = Velikonoce.AddDays(-46);
        }

        private DateTime DatumVelikonoc(int rok)
        {
            //Zdroj(8. 1. 2021): https://kalendar.beda.cz/ostatni-algoritmy-vypoctu-velikonocni-nedele
            var c = rok / 100;
            var n = rok % 19;
            var k = (c - 17) / 25;
            var i = (c - c / 4 - (c - k) / 3 + 19 * n + 15) % 30;
            i -= (i / 28) * (1 - (i / 28) * (29 / (i + 1)) * ((21 - n) / 11));
            var j = (rok + rok / 4 + i + 2 - c + (c / 4)) % 7;
            var l = i - j;
            var mesic = 3 + (l + 40) / 44;
            var den = l + 28 - 31 * (mesic / 4);

            return new DateTime(rok, mesic, den);
        }

        /// <summary>
        /// vypocita datum prvni nedele adventni ze zadaneho data
        /// </summary>
        /// <param name="datum">DateTime datum prvni nedele adventni</param>
        /// <returns></returns>
        private DateTime DatumPrvniAdventni(DateTime datum)
        {
            DateTime listopad30 = new DateTime(datum.Year, 11, 30);
            switch (listopad30.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return listopad30.AddDays(-1);
                case DayOfWeek.Tuesday:
                    return listopad30.AddDays(-2);
                case DayOfWeek.Wednesday:
                    return listopad30.AddDays(-3);
                case DayOfWeek.Thursday:
                    return listopad30.AddDays(3);
                case DayOfWeek.Friday:
                    return listopad30.AddDays(2);
                case DayOfWeek.Saturday:
                    return listopad30.AddDays(1);
                case DayOfWeek.Sunday:
                    return listopad30;
                default:
                    return new DateTime();
            }
        }

        /// <summary>
        /// Vypocita vzdalenost od 1. nedele adventni v tydnech
        /// </summary>
        /// <param name="datum">DateTime datum jehoz vzdalenost od 1. adventni pocitame</param>
        /// <returns>int pocet tydnu od 1.nedele adventni</returns>
        public int PoradiTydneVLiturgickemRoce(DateTime datum)
        {
            double vzdalenostOd1Adventni;

            //pokud je datum v danem roce po 1. adventni
            if (DatumPrvniAdventni(datum) <= datum)
            {
                //bude zaporne, a tak ho udelame kladnym
                vzdalenostOd1Adventni = -1 * DatumPrvniAdventni(datum).Subtract(datum).TotalDays;
            }
            else
            {
                //pokud je datum v danem roce pred 1. adventni spocitej vzdalenost od te lonske
                vzdalenostOd1Adventni = -1 * DatumPrvniAdventni(datum.AddYears(-1)).Subtract(datum).TotalDays;
            }

            return (int)(vzdalenostOd1Adventni / 7) + 1;
        }

        /// <summary>
        /// Z velikonoc a 1. advetni nedele urci NAZEV_DNE a ID pro tabulku SVATEK
        /// Vyplni promene NaevDne a TypSvatku
        /// </summary>
        /// <param name="datum"></param>
        /// <returns>int cislo svatku (ID v databazi v tabulce SVATEK)</returns>
        public int GetSvatekId(DateTime datum)
        {
            int svatekId;

            int poradiTydne = PoradiTydneVLiturgickemRoce(datum);
            if (datum.Equals(new DateTime(datum.Year, 1, 1)))
            {
                TypSvatku = NazevDne = "matkyBozi";
                svatekId = 11;
            }
            else if (datum.Equals(new DateTime(datum.Year, 1, 6)))
            {
                TypSvatku = NazevDne = "zjeveni";
                svatekId = 13;
            }
            else if (datum.Equals(new DateTime(datum.Year, 12, 25)))
            {
                TypSvatku = NazevDne = "narozeni";
                svatekId = 9;
            }
            else if (datum.Equals(new DateTime(datum.Year, 12, 8)))
            {
                TypSvatku = NazevDne = "bohorodicky";
                svatekId = 103;
            }
            //svata rodina
            else if (poradiTydne == 5 && datum.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                TypSvatku = NazevDne = "rodiny";
                svatekId = 10;
            }
            else if (poradiTydne == 5 && datum.Equals(new DateTime(datum.Year, 12, 30)))
            {
                TypSvatku = NazevDne = "rodiny";
                svatekId = 10;
            }
            else if (datum.Equals(new DateTime(datum.Year, 12, 27)))
            {
                TypSvatku = NazevDne = "jana";
                svatekId = 104;
            }
            else if (datum.Equals(new DateTime(datum.Year, 12, 28)))
            {
                TypSvatku = NazevDne = "mladatek";
                svatekId = 105;
            }
            //stepan
            else if (datum.Equals(new DateTime(datum.Year, 12, 26)))
            {
                TypSvatku = NazevDne = "stepana";
                svatekId = 106;
            }
            //nedele mezi 1.6. a 13.1.
            else if (datum > new DateTime(datum.Year, 1, 6) && datum < new DateTime(datum.Year, 1, 14) && datum.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                TypSvatku = NazevDne = "krtuPane";
                svatekId = 14;
            }
            //advent
            else if (poradiTydne <= 4)
            {
                TypSvatku = $"adventni{poradiTydne}";
                NazevDne = string.Concat((int)datum.DayOfWeek," ",TypSvatku);
                svatekId = poradiTydne;
            }
            else if (poradiTydne == 6 && datum.DayOfYear < 7)
            {
                TypSvatku = NazevDne = "2poNarozeniPane";
                svatekId = 12;
            }
            //else if (poradiTydne == 7 && datum.DayOfYear < 14)
            //    {nazevDne = "krtuPane";
            // svatekId = 14;}
            else if (poradiTydne == 52)
            {
                TypSvatku = NazevDne = "krale";
                svatekId = 107;
            }
            //velikonoce
            else if (datum == PopelecniStreda)
            {
                TypSvatku = NazevDne = nameof(PopelecniStreda);
                svatekId = 17;
            }
            else if (datum > PopelecniStreda && datum < Velikonoce.AddDays(7 * 10))
            {
                int dnuOdPopelecniStredy = datum.Subtract(PopelecniStreda).Days;
                if (dnuOdPopelecniStredy < 4)
                {
                    TypSvatku = NazevDne = (int)datum.DayOfWeek + "poPopelecni";
                    svatekId = (int)datum.DayOfWeek + 107; //107 bude ofset indexu v databazi pro dny po popelecni strede
                }
                else if ((dnuOdPopelecniStredy - 4) / 7 < 5)
                {
                    TypSvatku = NazevDne = "postni" + ((dnuOdPopelecniStredy - 4) / 7 + 1);
                    svatekId = (dnuOdPopelecniStredy - 4) / 7 + 1 + 17; //17 je posun, kde zacina id postnich tydnu
                }
                else if (dnuOdPopelecniStredy == 39)
                {
                    TypSvatku = NazevDne = "kvetna";
                    svatekId = 24;
                }
                else if (dnuOdPopelecniStredy < 43)
                {
                    TypSvatku = "svatehoTydne";
                    NazevDne = string.Concat((int)datum.DayOfWeek, " ",TypSvatku);
                    svatekId = 25;
                }
                else if (dnuOdPopelecniStredy == 43)
                {
                    TypSvatku = NazevDne = "zeleny";
                    svatekId = 26;
                }
                else if (dnuOdPopelecniStredy == 44)
                {
                    TypSvatku = NazevDne = "velky";
                    svatekId = 27;
                }
                else if (dnuOdPopelecniStredy == 45)
                {
                    TypSvatku = NazevDne = "bila";
                    svatekId = 28;
                }
                else if (dnuOdPopelecniStredy == 46)
                {
                    TypSvatku = NazevDne = "zmrtvychvstani";
                    svatekId = 30;
                }
                else if (dnuOdPopelecniStredy == 85)
                {
                    TypSvatku = NazevDne = "nanebevstoupeni";
                    svatekId = 38;
                }
                else if ((dnuOdPopelecniStredy - 4) / 7 < 13)
                {
                    TypSvatku = $"velikonocni{(dnuOdPopelecniStredy - 4) / 7 - 5}";
                    NazevDne = string.Concat((int)datum.DayOfWeek, " ",TypSvatku);
                    svatekId = (dnuOdPopelecniStredy - 4) / 7 - 5 + 30;//30 je offset indexu v databazi prvni velikonocni nedele
                }
                else if (dnuOdPopelecniStredy == 95)
                {
                    TypSvatku = NazevDne = "seslaniDucha";
                    svatekId = 39;
                }
                else if (dnuOdPopelecniStredy == 102)
                {
                    TypSvatku = NazevDne = "nejsvetejsi3";
                    svatekId = 40;
                }
                else if (dnuOdPopelecniStredy == 106)
                {
                    TypSvatku = NazevDne = "telaKrve";
                    svatekId = 41;
                }
                else if (dnuOdPopelecniStredy == 114)
                {
                    TypSvatku = NazevDne = "srdceJezisova";
                    svatekId = 42;
                }
                else if (dnuOdPopelecniStredy == 115)
                {
                    TypSvatku = NazevDne = "neposkvrneneSrdce";
                    svatekId = 43;
                }
                else
                {
                    TypSvatku = $"mezidobi{poradiTydne - 18}";
                    NazevDne = string.Concat((int)datum.DayOfWeek, " ", TypSvatku);
                    svatekId = poradiTydne - 18 + 43;// 43 je ofset indexu v databazi pro druhou nedeli v mezidobi
                }

            }
            //po Velikonocich
            else if (datum > Velikonoce.AddDays(7 * 10))//&& datum < PrvniAdventni)
            {
                //datum = datum.AddDays(-7 * 13.5);
                {
                    TypSvatku = $"mezidobi{poradiTydne - 18}";
                    NazevDne = string.Concat((int)datum.DayOfWeek," ",TypSvatku);
                    svatekId = poradiTydne - 18 + 43;// 43 je ofset indexu v databazi pro druhou nedeli v mezidobi
                }
            }

            //pred Velikonocemi
            else
            {
                if (new DateTime(datum.Year - 1, 12, 24).DayOfWeek.Equals(DayOfWeek.Sunday))
                    poradiTydne++;
                //datum = datum.AddDays(-7 * 8);
                {
                    TypSvatku = $"mezidobi{poradiTydne - 6}";
                    NazevDne = string.Concat((int)datum.DayOfWeek, " ", TypSvatku);
                    svatekId = poradiTydne - 6 + 43;
                }
            }

            return svatekId;
        }

        public Dictionary<int, string> DnyTydne { get; set; } = new() { { 1, "pondělí" }, { 2, "úterý" }, { 3, "středa" }, { 4, "čtvrtek" }, { 5, "pátek" }, { 6, "sobota" }, { 0, "neděle" } };

        ///END liturgickyRok class
    }

    public enum Tyden
    {
        pondělí = 1,
        úterý = 2,
        středa = 3,
        čtvrtek = 4,
        pátek = 5,
        sobota = 6,
        neděle = 0
    }

    enum Advent
    {
        adventni1 = 1,//"adventni1",
        adventni2 = 2,
        adventni3 = 3,
        adventni4 = 4,
    }
    enum Vanocni
    {
        narozeni = 25,
        stepana = 26,
        jana = 27,
        mladate = 28,
        svateRodiny = 5,
        poNarozeniPane = 6,
        krtuPnae = 7
    }
    enum Postni
    {
        popelecni = 0,
        postni1 = 1,
        postni2 = 2,
        postni3 = 3,
        postni4 = 4,
        postni5 = 5,
        postni6 = 6,
        kvetna = 7,
        zeleny = 8,
        velky = 9,
        bila = 10
    }
    enum Velikonocni
    {
        zmrtvychvstani = 0,
        velikonocni1 = 1,
        velikonocni2 = 2,
        velikonocni3 = 3,
        velikonocni4 = 4,
        velikonocni5 = 5,
        velikonocni6 = 6,
        velikonocni7 = 7,
        nanebevstoupeni = 10
    }
}
