
var builder = WebApplication.CreateBuilder(args);

var startupWeb = new Apparka.WebApi.Startup(builder.Configuration);

var startupServices = new Apparka.Services.Startup(builder.Configuration);

startupServices.ConfigureServices(builder.Services);
startupWeb.ConfigureServices(builder.Services);


var app = builder.Build();

var logger = app.Services.GetService(typeof(ILogger<Apparka.WebApi.Startup>)) as ILogger<Apparka.WebApi.Startup>;
startupWeb.Configure(app, app.Environment, logger);


app.Run();
