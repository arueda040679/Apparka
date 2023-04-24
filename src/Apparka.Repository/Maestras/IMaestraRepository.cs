using Apparka.Repository.Maestras.Entities;

namespace Apparka.Repository.Maestras
{
    public interface IMaestraRepository
    {
        Task<CajaEntity> GetCajaById(int idPlaya, string idCaja);
        Task<ComercioEntity> GetComercioById(string idComercio);
        Task<ComercioPlayaEntity> GetComercioPlayaById(string idComercio, int idPlaya);
        Task<PlayaEntity> GetPlayaById(int idPlaya);
        Task<ParametroCajaEntity> GetParametroCajaById(int idPlaya, string idCaja, string parametro);
        Task<AccesoEntity> GetAccesoById(int idPlaya, int idAcceso);
        Task<PromocionEntity> GetPromocionById(int idPlaya, int idPromocion);
        Task<PromocionEntity> GetPromocionByIdPlaya(int idPlaya);
        Task<PromocionEntity> GetPromocionByIdNumeroTarifa(int idPlaya, int idNumeroTarifa);
        Task<ToleranciaEntity> GetToleranciaById(int idPlaya, int idAcceso, int idTipoTolerancia);
    }
}
