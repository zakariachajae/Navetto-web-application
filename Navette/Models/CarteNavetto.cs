namespace Navette.Models
{
    public class CarteNavetto
    {
        public int Id { get; set; }
        public string ProprietaireCarte { get; set; }
        public string Type { get; set; }
        public int NumeroCarte { get; set; }
        public string DateExpiration { get; set; }
        public int CVV { get; set; }
    }
}