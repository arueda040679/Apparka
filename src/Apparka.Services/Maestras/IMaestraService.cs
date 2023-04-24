using Apparka.Core.Maestras;

namespace Apparka.Services.Maestras
{
    public interface IMaestraService
    {
        Task<Comercio> GetComercio(string idComercio);
        Task<ComercioPlaya> GetComercioPlaya(string idComercio, int idPlaya);
        Task<Playa> GetPlaya(int idPlaya);
        Task<Caja> GetCaja(int idPlaya, string idCaja);
        Task<ParametroCaja> GetParametroCajaById(int idPlaya, string idCaja, string parametro);
        Task<Acceso> GetAccesoById(int idPlaya, int idAcceso);
        Task<Promocion> GetPromocionById(int idPlaya, int idPromocion);
        Task<Promocion> GetPromocionByIdPlaya(int idPlaya);
        Task<Promocion> GetPromocionByIdNumeroTarifa(int idPlaya, int idNumeroTarifa);
        Task<Tolerancia> GetToleranciaById(int idPlaya, int idAcceso, int idTipoTolerancia);
    }
}
