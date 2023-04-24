namespace Apparka.Repository.Maestras.Entities
{
    public class PromocionEntity
    {

        public int IdPlaya { get; set; }
        public int IdPromocion { get; set; }
        public int? IdNumeroTarifa { get; set; }
        public bool FlagMovil { get; set; }
        public bool FlagActivo { get; set; }
        public bool AnularAlFinalizar { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

    }
}
