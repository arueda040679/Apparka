using Apparka.Repository.Maestras.Entities;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Apparka.Repository.Maestras
{
    public class MaestraRepositoryImpl : IMaestraRepository
    {

        private readonly ILogger<MaestraRepositoryImpl> logger;
        private readonly IConfiguration configuration;
        private readonly DbCommonUtil dbCommonUtil;
        private readonly IMapper mapper;

        public MaestraRepositoryImpl(ILogger<MaestraRepositoryImpl> logger,
             IConfiguration configuration,
            DbCommonUtil dbCommonUtil,
            IMapper mapper)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.dbCommonUtil = dbCommonUtil;
            this.mapper = mapper;
        }

        public async Task<AccesoEntity> GetAccesoById(int idPlaya, int idAcceso)
        {
            logger.LogDebug("getAccesoById");
            AccesoEntity accesoEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdAcceso", idAcceso),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_AccesoObtenerXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    accesoEntity = new AccesoEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),                        
                        IdAcceso = reader.GetInt32(reader.GetOrdinal("IdAcceso")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        IdPromocion = reader.IsDBNull(reader.GetOrdinal("IdPromocion")) ? null : reader.GetInt32(reader.GetOrdinal("IdPromocion")),
                    };
                }
            }

            return accesoEntity;
        }

        public async Task<CajaEntity> GetCajaById(int idPlaya, string idCaja)
        {
            logger.LogDebug("getCajaById");
            CajaEntity cajaEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdCaja", idCaja),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_CajaAccesoObtenerXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    cajaEntity = new CajaEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdCaja = reader.GetString(reader.GetOrdinal("IdCaja")),
                        IdAcceso = reader.GetInt32(reader.GetOrdinal("IdAcceso")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        FlagAcceso = reader.GetBoolean(reader.GetOrdinal("FlagAcceso")),
                    };
                }
            }

            return cajaEntity;
        }

        public async Task<ComercioEntity> GetComercioById(string idComercio)
        {
            logger.LogDebug("getComercioById");

            ComercioEntity comercioEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdComercio", idComercio)
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_ComercioXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    comercioEntity = new ComercioEntity()
                    {
                        IdComercio = reader.GetString(reader.GetOrdinal("IdComercio")),
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                    };
                }
            }

            return comercioEntity;

        }

        public async Task<ComercioPlayaEntity> GetComercioPlayaById(string idComercio, int idPlaya)
        {

            logger.LogDebug("getComercioPlayaById");

            ComercioPlayaEntity comercioPlayaEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdComercio", idComercio),
                                            new SqlParameter("@IdPlaya", idPlaya)
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_UsuarioAPPARKAComercioPlayaXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    comercioPlayaEntity = new ComercioPlayaEntity()
                    {
                        IdComercio = reader.GetString(reader.GetOrdinal("IdComercio")),
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdCaja = reader.GetString(reader.GetOrdinal("IdCaja")),
                        FlagWallet = reader.GetBoolean(reader.GetOrdinal("FlagWallet")),
                        FlagCentral = reader.GetBoolean(reader.GetOrdinal("FlagCentral")),
                        FlagCE = reader.GetBoolean(reader.GetOrdinal("FlagCE")),
                    };
                }
            }

            return comercioPlayaEntity;


        }

        public async Task<ParametroCajaEntity> GetParametroCajaById(int idPlaya, string idCaja, string parametro)
        {
            logger.LogDebug("getParametroCajaById");

            ParametroCajaEntity parametroCajaEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdCaja", idCaja),
                                            new SqlParameter("@Parametro", parametro),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_ParametroCajaObtenerXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    parametroCajaEntity = new ParametroCajaEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdCaja = reader.GetString(reader.GetOrdinal("IdCaja")),
                        Parametro = reader.GetString(reader.GetOrdinal("Parametro")),
                        Valor = reader.GetString(reader.GetOrdinal("Valor")),
                    };
                }
            }

            return parametroCajaEntity;
        }

        public async Task<PlayaEntity> GetPlayaById(int idPlaya)
        {
            logger.LogDebug("getPlayaById");

            PlayaEntity playaEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya)
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_PlayaOperacionObtenerXId", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    playaEntity = new PlayaEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        IdOperacion = reader.IsDBNull(reader.GetOrdinal("IdOperacion")) ? null : reader.GetInt32(reader.GetOrdinal("IdOperacion")),
                        FlagOperacion = reader.IsDBNull(reader.GetOrdinal("FlagOperacion")) ? null : reader.GetBoolean(reader.GetOrdinal("FlagOperacion")),
                        FlagPago = reader.IsDBNull(reader.GetOrdinal("MovFlagPagar")) ? null : reader.GetBoolean(reader.GetOrdinal("MovFlagPagar")),
                    };
                }
            }

            return playaEntity;
        }

        public async Task<PromocionEntity> GetPromocionById(int idPlaya, int idPromocion)
        {
            logger.LogDebug("getPromocionById");
            PromocionEntity promocionEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdPromocion", idPromocion),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_PromocionObtenerXIdPromocionAPPARKA", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    promocionEntity = new PromocionEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdPromocion = reader.GetInt32(reader.GetOrdinal("IdPromocion")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        FlagMovil = reader.GetBoolean(reader.GetOrdinal("FlagMovil")),
                        AnularAlFinalizar = reader.GetBoolean(reader.GetOrdinal("AnularAlFinalizar")),
                        IdNumeroTarifa = reader.IsDBNull(reader.GetOrdinal("IdNumeroTarifa")) ? null : reader.GetInt32(reader.GetOrdinal("IdNumeroTarifa")),
                        FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? null: reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                        FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                    };
                }
            }

            return promocionEntity;
        }

        public async Task<PromocionEntity> GetPromocionByIdNumeroTarifa(int idPlaya, int idNumeroTarifa)
        {
            logger.LogDebug("getPromocionByIdNumeroTarifa");
            PromocionEntity promocionEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdNumeroTarifa", idNumeroTarifa),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_PromocionObtenerXIdNumeroTarifaAPPARKA", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    promocionEntity = new PromocionEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdPromocion = reader.GetInt32(reader.GetOrdinal("IdPromocion")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        FlagMovil = reader.GetBoolean(reader.GetOrdinal("FlagMovil")),
                        AnularAlFinalizar = reader.GetBoolean(reader.GetOrdinal("AnularAlFinalizar")),
                        IdNumeroTarifa = reader.IsDBNull(reader.GetOrdinal("IdNumeroTarifa")) ? null : reader.GetInt32(reader.GetOrdinal("IdNumeroTarifa")),
                        FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                        FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                    };
                }
            }

            return promocionEntity;
        }

        public async Task<PromocionEntity> GetPromocionByIdPlaya(int idPlaya)
        {
            logger.LogDebug("getPromocionByIdPlaya");
            PromocionEntity promocionEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),                                            
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_PromocionObtenerXIdPlayaAPPARKA", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    promocionEntity = new PromocionEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdPromocion = reader.GetInt32(reader.GetOrdinal("IdPromocion")),
                        FlagActivo = reader.GetBoolean(reader.GetOrdinal("FlagActivo")),
                        FlagMovil = reader.GetBoolean(reader.GetOrdinal("FlagMovil")),
                        AnularAlFinalizar = reader.GetBoolean(reader.GetOrdinal("AnularAlFinalizar")),
                        IdNumeroTarifa = reader.IsDBNull(reader.GetOrdinal("IdNumeroTarifa")) ? null : reader.GetInt32(reader.GetOrdinal("IdNumeroTarifa")),
                        FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                        FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                    };
                }
            }

            return promocionEntity;
        }

        public async Task<ToleranciaEntity> GetToleranciaById(int idPlaya, int idAcceso, int idTipoTolerancia)
        {
            logger.LogDebug("getToleranciaById");
            ToleranciaEntity toleranciaEntity = null;

            SqlParameter[] parameters = {
                                            new SqlParameter("@IdPlaya", idPlaya),
                                            new SqlParameter("@IdAcceso", idAcceso),
                                            new SqlParameter("@IdTipoTolerancia", idTipoTolerancia),
                                        };

            using (var reader = await dbCommonUtil.ExecuteReaderAsync("USP_ToleranciaObtenerActivoAPPARKA", System.Data.CommandType.StoredProcedure, parameters))
            {
                if (await reader.ReadAsync())
                {
                    toleranciaEntity = new ToleranciaEntity()
                    {
                        IdPlaya = reader.GetInt32(reader.GetOrdinal("IdPlaya")),
                        IdAcceso = reader.GetInt32(reader.GetOrdinal("IdAcceso")),
                        IdTipoTolerancia = reader.GetInt32(reader.GetOrdinal("IdTipoTolerancia")),
                        Minutos = reader.GetInt32(reader.GetOrdinal("Minutos")),
                    };
                }
            }

            return toleranciaEntity;
        }
    }
}



