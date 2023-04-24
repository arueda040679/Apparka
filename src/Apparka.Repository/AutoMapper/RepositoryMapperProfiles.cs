using AutoMapper;
using Apparka.Core.Maestras;
using Apparka.Repository.Maestras.Entities;

namespace Apparka.WebApi.AutoMapper;

public class RepositoryMapperProfiles : Profile
{

    public RepositoryMapperProfiles()
    {

        this.CreateMap<ComercioEntity, Comercio>();
        this.CreateMap<ComercioPlayaEntity, ComercioPlaya>();
        this.CreateMap<PlayaEntity, Playa>();
        this.CreateMap<CajaEntity, Caja>();
        this.CreateMap<ParametroCajaEntity, ParametroCaja>();
        this.CreateMap<AccesoEntity, Acceso>();
        this.CreateMap<PromocionEntity, Promocion>();
        this.CreateMap<ToleranciaEntity, Tolerancia>();

    }
}
