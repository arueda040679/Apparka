namespace Apparka.Repository.Maestras.Entities
{
    public class PlayaEntity
    {

        public int IdPlaya { get; set; }
        public string Nombre { get; set; }
        public bool FlagActivo { get; set; }
        public int? IdOperacion { get; set; }
        public bool? FlagOperacion { get; set; }
        public bool? FlagPago { get; set; }

    }
}
