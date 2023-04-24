using Apparka.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Apparka.Repository.Maestras;

namespace Apparka.Services;

public class Startup{

    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration){
        this.configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services){
        //IoD
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<DbCommonUtil>();
        services.AddSingleton<IMaestraRepository, MaestraRepositoryImpl>();

    }


    
}
