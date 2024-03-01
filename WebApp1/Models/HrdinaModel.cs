using WebApp1.Models.Database;

namespace WebApp1.Models
{
    public class HrdinaModel
    {
        public Hrdina Hrdina { get; set; }

        public List<HrdinaPovolani> HrdinovaPovolani { get; set; }

        public List<Povolani> Povolani { get; set; }

        public List<Schopnost> Schopnosti { get; set; }
    }
}
