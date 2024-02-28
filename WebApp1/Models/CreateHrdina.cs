using WebApp1.Models.Database;

namespace WebApp1.Models
{
    public class CreateHrdina
    {
        public Hrdina Hrdina { get; set; }

        public List<HrdinaPovolani> HrdinaPovolani { get; set; }

        public List<Povolani> Povolani { get; set; }
    }
}
