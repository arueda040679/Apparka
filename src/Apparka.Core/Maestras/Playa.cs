namespace Apparka.Core.Maestras
{
    public class Playa
    {
        public int IdPlaya { get; set; }
        public string Nombre { get; set; }
        public bool FlagActivo { get; set; }
        public int? IdOperacion { get; set; }
        public bool? FlagOperacion { get; set; }
        public bool? FlagPago { get; set; }
    }
}
