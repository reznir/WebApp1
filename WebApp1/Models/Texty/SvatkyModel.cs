namespace WebApp1.Models.Texty
{
    /// <summary>
    /// Model for displaying Doby and Svatky
    /// </summary>
    public class SvatkyModel
    {
        public SvatkyModel()
        {
            Doby = new List<Doba>();
            Svatky = new List<Svatek>();
            LitTexty = new List<LitText>();
        }

        public List<Doba> Doby { get; set; }

        public List<Svatek> Svatky { get; set;}

        public List<LitText> LitTexty { get; set; }

    }
}
