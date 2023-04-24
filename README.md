## Create solution
```
dotnet new sln -o Apparka
cd src
mkdir src

dotnet new webapi -lang C# -o src/Apparka.WebApi
dotnet sln add src/Apparka.WebApi/Apparka.WebApi.csproj
dotnet add src/Apparka.WebApi/Apparka.WebApi.csproj reference src/Apparka.Core/Apparka.Core.csproj
dotnet add src/Apparka.WebApi/Apparka.WebApi.csproj reference src/Apparka.Services/Apparka.Services.csproj

dotnet new mstest -o src/Apparka.WebApi.Test
dotnet add src/Apparka.WebApi.Test/Apparka.WebApi.Test.csproj reference src/Apparka.WebApi/Apparka.WebApi.csproj
dotnet add src/Apparka.WebApi.Test/Apparka.WebApi.Test.csproj reference src/Apparka.Services/Apparka.Services.csproj
dotnet add package Moq --version 4.18.2



dotnet new classlib -lang C# -o src/Apparka.Services
dotnet sln add src/Apparka.Services/Apparka.Services.csproj
dotnet add src/Apparka.Services/Apparka.Services.csproj reference src/Apparka.Core/Apparka.Core.csproj
dotnet add src/Apparka.Services/Apparka.Services.csproj reference src/Apparka.Repository/Apparka.Repository.csproj

dotnet new mstest -o src/Apparka.Services.Test
dotnet add src/Apparka.Services.Test/Apparka.Services.Test.csproj reference src/Apparka.Services/Apparka.Services.csproj


dotnet new classlib -lang C# -o src/Apparka.Repository
dotnet sln add src/Apparka.Repository/Apparka.Repository.csproj
dotnet add src/Apparka.Repository/Apparka.Repository.csproj reference src/Apparka.Core/Apparka.Core.csproj

dotnet new classlib -lang C# -o src/Apparka.Core
dotnet sln add src/Apparka.Core/Apparka.Core.csproj


dotnet new webapi -lang C# -o src/Apparka.Mock
dotnet sln add src/Apparka.Mock/Apparka.Mock.csproj

```


## Build and test
```
dotnet test src/Apparka.WebApi.Test/Apparka.WebApi.Test.csproj
dotnet build
dotnet run --project src/Apparka.WebApi/Apparka.WebApi.csproj
```


## Database
```
use WEBPORTALESNET_RESTORE;

sp_helptext USP_UsuarioAPPARKAVPaymentTokenRegistrar;

use WEBPORTALESNET_RESTORE;
select top 10 * from UsuarioAPPARKAVPaymentToken WHERE Token='123589';    
67848

use WEBPORTALESNET_RESTORE;
sp_helptext 'USP_UsuarioAPPARKAVPaymentTokenEliminar';

use WEBPORTALESNET_RESTORE;
sp_helptext 'USP_AppOperacionObtenerXId';



```

SERVICE APP: devapibackendapparka-csonic
URL: https://devapibackendapparka-csonic.azurewebsites.net/swagger/index.html

====

https://learn.microsoft.com/en-us/answers/questions/888691/how-to-set-up-custom-claims-in-azure-ad-b2c-applic.html

https://jwt.ms/

https://www.hossambarakat.net/2020/08/14/azure-b2c-client-credentials/

==
<fragment>
	<validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid." require-expiration-time="true" require-signed-tokens="true" clock-skew="300">
		<openid-config url="https://login.microsoftonline.com/e78d9f1d-6369-43fb-90cc-3415db922125/v2.0/.well-known/openid-configuration" />
	</validate-jwt>
</fragment>
===


<fragment>
	<validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid." require-expiration-time="true" require-signed-tokens="true" clock-skew="300">
		<openid-config url="https://login.microsoftonline.com/e78d9f1d-6369-43fb-90cc-3415db922125/v2.0/.well-known/openid-configuration" />
	</validate-jwt>
</fragment>

===

jockey-backend-api-url

<fragment>
	<set-backend-service backend-id="aparka-api-dev-backend" />
</fragment>


<policies>
    <inbound>
        <set-backend-service base-url="aparka-api-dev-backend" />
        <base />
    </inbound>
    <outbound>
        <base />
    </outbound>
</policies>




my-api1
    f9c265cf-58e8-41c3-8f28-0112499818a0
    Client Credentials:
        HEL8Q~ycPYmqWcB-VhKGlloYSNTM8_Opl.156bte
        daacf4fe-e1b6-4246-b73f-a74130f6ffcc

        Application ID URI: https://CSonicB2C.onmicrosoft.com/9d91f4ff-9b74-4dc5-b1b4-f288a9e92205

    


    https://CSonicB2C.b2clogin.com/CSonicB2C.onmicrosoft.com/oauth2/v2.0/token
    https://CSonicB2C.b2clogin.com/CSonicB2C.onmicrosoft.com/<policy-name>/oauth2/v2.0/token

    https://CSonicB2C.b2clogin.com/CSonicB2C.onmicrosoft.com/oauth2/v2.0/token
    ClientId = "C8s8Q~0aRLGmldgUDZSwR-j6.CFLHBSXJzrzEaIZ",
      ClientSecret = "8cb456af-047a-4af4-9aff-0cc3a6678adb",
      Scope = "https://CSonicB2C.onmicrosoft.com/9d91f4ff-9b74-4dc5-b1b4-f288a9e92205/.default"

curl --location --request POST 'https://login.microsoftonline.com/CSonicB2C.onmicrosoft.com/oauth2/v2.0/token' \
    --header 'Content-Type: application/x-www-form-urlencoded' \
    --header 'Cookie: fpc=AlIMqDtLaTBPjkpDdYI4cZr8sMTeAgAAAAFet9oOAAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd' \
    --data-urlencode 'scope=https://CSonicB2C.onmicrosoft.com/9d91f4ff-9b74-4dc5-b1b4-f288a9e92205/.default' \
    --data-urlencode 'grant_type=client_credentials' \
    --data-urlencode 'client_id=9d91f4ff-9b74-4dc5-b1b4-f288a9e92205' \
    --data-urlencode 'client_secret=HEL8Q~ycPYmqWcB-VhKGlloYSNTM8_Opl.156bte'


my-app2
    clienteId: 478b1a97-e18b-4eff-bdb8-1a079d222a67
    ClienteSeccret: LE08Q~IIpjTkn.RGs2i1rlMNxGfuMV5lAQSIKbwb

    curl --location --request POST 'https://login.microsoftonline.com/CSonicB2C.onmicrosoft.com/oauth2/v2.0/token' \
    --header 'Content-Type: application/x-www-form-urlencoded' \
    --data-urlencode 'scope=https://CSonicB2C.onmicrosoft.com/478b1a97-e18b-4eff-bdb8-1a079d222a67/.default' \
    --data-urlencode 'grant_type=client_credentials' \
    --data-urlencode 'client_id=478b1a97-e18b-4eff-bdb8-1a079d222a67' \
    --data-urlencode 'client_secret=LE08Q~IIpjTkn.RGs2i1rlMNxGfuMV5lAQSIKbwb'