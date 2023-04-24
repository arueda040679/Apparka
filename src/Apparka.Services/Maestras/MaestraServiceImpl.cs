using Apparka.Core.Maestras;
using Apparka.Repository.Maestras;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Apparka.Services.Maestras
{
    public class MaestraServiceImpl : IMaestraService
    {
        private readonly ILogger<MaestraServiceImpl> logger;
        private readonly IMaestraRepository maestraRepository;
        private readonly IMapper mapper;

        public MaestraServiceImpl(ILogger<MaestraServiceImpl> logger,
                            IMaestraRepository maestraRepository,
                            IMapper mapper)
        {
            this.logger = logger;
            this.maestraRepository = maestraRepository;
            this.mapper = mapper;
        }

        public async Task<Comercio> GetComercio(string idComercio)
        {
            using (logger.BeginScope("GetComercio"))
            {
                var commerceEntity = await maestraRepository.GetComercioById(idComercio);

                return mapper.Map<Comercio>(commerceEntity);

            }
        }

        public async Task<ComercioPlaya> GetComercioPlaya(string idComercio, int idPlaya)
        {
            using (logger.BeginScope("GetComercioPlaya"))
            {
                var commerceParking = await maestraRepository.GetComercioPlayaById(idComercio, idPlaya);

                return mapper.Map<ComercioPlaya>(commerceParking);

            }
        }

        public async Task<Playa> GetPlaya(int idPlaya)
        {
            using (logger.BeginScope("GetPlaya"))
            {
                var parkingEntity = await maestraRepository.GetPlayaById(idPlaya);

                return mapper.Map<Playa>(parkingEntity);

            }
        }

        public async Task<Caja> GetCaja(int idPlaya, string idCaja)
        {

            using (logger.BeginScope("GetCaja"))
            {
                var cajaEntity = await maestraRepository.GetCajaById(idPlaya, idCaja);

                return mapper.Map<Caja>(cajaEntity);

            }

        }

        public async Task<ParametroCaja> GetParametroCajaById(int idPlaya, string idCaja, string parametro)
        {
            using (logger.BeginScope("GetParametroCaja"))
            {
                var parametroCajaEntity = await maestraRepository.GetParametroCajaById(idPlaya, idCaja, parametro);

                return mapper.Map<ParametroCaja>(parametroCajaEntity);

            }
        }

        public async Task<Acceso> GetAccesoById(int idPlaya, int idAcceso)
        {
            using (logger.BeginScope("GetAcceso"))
            {
                var accesoEntity = await maestraRepository.GetAccesoById(idPlaya, idAcceso);

                return mapper.Map<Acceso>(accesoEntity);

            }
        }

        public async Task<Promocion> GetPromocionById(int idPlaya, int idPromocion)
        {
            using (logger.BeginScope("GetPromocionById"))
            {
                var promocionEntity = await maestraRepository.GetPromocionById(idPlaya, idPromocion);

                return mapper.Map<Promocion>(promocionEntity);

            }
        }

        public async Task<Promocion> GetPromocionByIdPlaya(int idPlaya)
        {
            using (logger.BeginScope("GetPromocionByIdPlaya"))
            {
                var promocionEntity = await maestraRepository.GetPromocionByIdPlaya(idPlaya);
                return mapper.Map<Promocion>(promocionEntity);
            }
        }

        public async Task<Promocion> GetPromocionByIdNumeroTarifa(int idPlaya, int idNumeroTarifa)
        {
            using (logger.BeginScope("GetPromocionByIdNumeroTarifa"))
            {
                var promocionEntity = await maestraRepository.GetPromocionByIdNumeroTarifa(idPlaya, idNumeroTarifa);
                return mapper.Map<Promocion>(promocionEntity);
            }
        }

        public async Task<Tolerancia> GetToleranciaById(int idPlaya, int idAcceso, int idTipoTolerancia)
        {
            using (logger.BeginScope("GetToleranciaById"))
            {
                var toleranciaEntity = await maestraRepository.GetToleranciaById(idPlaya, idAcceso, idTipoTolerancia);
                return mapper.Map<Tolerancia>(toleranciaEntity);
            }
        }
    }
}
