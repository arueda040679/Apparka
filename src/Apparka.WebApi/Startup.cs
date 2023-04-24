using System.Security.Authentication;
using System.Text.Json.Serialization;
using Apparka.Services.Maestras;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace Apparka.WebApi;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IConfiguration Configuration { get { return this.configuration; } }

    public void ConfigureServices(IServiceCollection services)
    {


        // Add services to the container.
      
        // Configurar Azure B2C

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.Authority = Configuration["AzureAdB2C:Authority"];

              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  ValidAudiences = Configuration.GetSection("AzureAdB2C:Audiences").Get<string[]>().ToList(),
                  ValidIssuer = Configuration["AzureAdB2C:ValidIssuer"],
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,

              };
          });

       

        //Delete null fields
        services.AddControllers().AddJsonOptions(option =>
        {
            option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            
            //settings.NullValueHandling = NullValueHandling.Ignore;
            //settings.DefaultValueHandling = DefaultValueHandling.Ignore;
        });


        services.AddSwaggerGen(options =>{

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });


        });

        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();

        //IoD

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IMaestraService, MaestraServiceImpl>();


        //var messageConfig = new MessageConfig();
        //configuration.GetSection("Bus").Bind(messageConfig, options => options.BindNonPublicProperties = true);
            
        
               


        // The following line enables Application Insights telemetry collection.
        services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:ConnectionString"]);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;
            app.UseDeveloperExceptionPage();
        }
        else {

            app.UseDeveloperExceptionPage();
        }

        app.UseHttpLogging();


        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();

        app.UseCors();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
       {
           endpoints.MapControllers();
       });


     


    }

}