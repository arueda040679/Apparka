using Apparka.Services.Maestras;
using Apparka.WebApi.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Apparka.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaestraController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ILogger<MaestraController> logger;
        private readonly IMaestraService maestraService;


        public MaestraController(IMapper mapper,
                                ILogger<MaestraController> logger,
                                IMaestraService maestraService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.maestraService = maestraService;
        }

        [HttpPost("getMaestra")]
        public async Task<ActionResult<ResponseDto>> GetMaestra()
        {

            ResponseDto responseDto;

            try
            {
                //Obtener comercio
                var idComercio = "999092";
                var comercio = await this.maestraService.GetComercio(idComercio);
                idComercio = comercio.IdComercio;
                var idProducto = comercio.IdProducto;
                var flagActivo = comercio.FlagActivo;

                //Obtener comercio playa
                idComercio = "999092";
                var idPlaya = 224;
                var comercioPlaya = await this.maestraService.GetComercioPlaya(idComercio, idPlaya);
                idComercio = comercioPlaya.IdComercio;
                idPlaya = comercioPlaya.IdPlaya;
                var idCaja = comercioPlaya.IdCaja;
                var flagCentral = comercioPlaya.FlagCentral;
                var flagCE = comercioPlaya.FlagCE;
                var flagWallet = comercioPlaya.FlagWallet;

                //ObtenerPlaya
                idPlaya = 224;
                var playa = await this.maestraService.GetPlaya(idPlaya);
                idPlaya = playa.IdPlaya;
                var playaNombre = playa.Nombre;
                flagActivo = playa.FlagActivo;
                var idOperacion = playa.IdOperacion;
                var flagOperacion = playa.FlagOperacion;

                //ObtenerCaja
                idPlaya = 224;
                idCaja = "666666666666";
                var caja = await this.maestraService.GetCaja(idPlaya, idCaja);
                idPlaya = caja.IdPlaya;
                idCaja = caja.IdCaja;
                var idAcceso = caja.IdAcceso;
                flagActivo = caja.FlagActivo;
                var flagAcceso = caja.FlagAcceso;

                //ObtenerParametroCaja
                idPlaya = 224;
                idCaja = "666666666666";
                var parametro = "ToleranciaSalida";
                var parametroCaja = await this.maestraService.GetParametroCajaById(idPlaya, idCaja, parametro);
                idPlaya = parametroCaja.IdPlaya;
                idCaja = parametroCaja.IdCaja;
                parametro = parametroCaja.Parametro;
                var valor = parametroCaja.Valor;

                //ObtenerAcceso
                idPlaya = 224;
                idAcceso = 14;
                var acceso = await this.maestraService.GetAccesoById(idPlaya, idAcceso);
                idPlaya = acceso.IdPlaya;
                idAcceso = acceso.IdAcceso;
                flagAcceso = acceso.FlagActivo;
                var IdPromocion = acceso.IdPromocion;


                //ObtenerPromocion x IdPromocion
                idPlaya = 224;
                var idPromocion = 3;
                var promocion = await this.maestraService.GetPromocionById(idPlaya, idPromocion);
                idPlaya = promocion.IdPlaya;
                idPromocion = promocion.IdPromocion;
                var idNumeroTarifa = promocion.IdNumeroTarifa;
                var flagMovil = promocion.FlagMovil;
                flagAcceso = promocion.FlagActivo;
                var anularAlFinalizar = promocion.AnularAlFinalizar;
                var fechaInicio = promocion.FechaInicio;
                var fechaFin = promocion.FechaFin;

                //ObtenerPromocion x IdPlaya
                idPlaya = 224;
                promocion = await this.maestraService.GetPromocionByIdPlaya(idPlaya);
                idPlaya = promocion.IdPlaya;
                idPromocion = promocion.IdPromocion;
                idNumeroTarifa = promocion.IdNumeroTarifa;
                flagMovil = promocion.FlagMovil;
                flagAcceso = promocion.FlagActivo;
                anularAlFinalizar = promocion.AnularAlFinalizar;
                fechaInicio = promocion.FechaInicio;
                fechaFin = promocion.FechaFin;

                //ObtenerPromocion x IdNumeroTarifa
                idPlaya = 224;
                idNumeroTarifa = 1;
                promocion = await this.maestraService.GetPromocionByIdNumeroTarifa(idPlaya, idNumeroTarifa.Value);
                idPlaya = promocion.IdPlaya;
                idPromocion = promocion.IdPromocion;
                idNumeroTarifa = promocion.IdNumeroTarifa;
                flagMovil = promocion.FlagMovil;
                flagAcceso = promocion.FlagActivo;
                anularAlFinalizar = promocion.AnularAlFinalizar;
                fechaInicio = promocion.FechaInicio;
                fechaFin = promocion.FechaFin;


                //ObtenerTolerancia
                idPlaya = 224;
                idAcceso = 14;
                var idTipoTolerancia = 1;
                var tolerancia = await this.maestraService.GetToleranciaById(idPlaya, idAcceso, idTipoTolerancia);
                idPlaya = tolerancia.IdPlaya;
                idAcceso = tolerancia.IdAcceso;
                idTipoTolerancia = tolerancia.IdTipoTolerancia;
                var minutos = tolerancia.Minutos;
               


                responseDto = new ResponseDto()
                {
                    validacion = ResponseDto.OK,
                    mensaje = "OK"
                };


            }
            catch (Exception ex)
            {
                this.logger.LogError("registrarToken", ex);
                responseDto = new ResponseDto()
                {
                    validacion = ResponseDto.ERROR,
                    mensaje = "Error registrando token"
                };
            }


            return Ok(responseDto);

        }

    }
}
