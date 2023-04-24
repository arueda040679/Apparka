package customers

import io.gatling.core.Predef._
import io.gatling.http.Predef._
import io.gatling.jdbc.Predef._

import scala.concurrent.duration._
import java.time.format.DateTimeFormatter
import scala.util.Random
import org.apache.commons.lang3.RandomStringUtils


class CustomersTest extends Simulation {
  
  	
   val feederCorrelationId01 = Iterator.continually(Map("uuid" -> (Random.alphanumeric.take(40).mkString)))
   val feederCorrelationId02 = Iterator.continually(Map("uuid" -> ( RandomStringUtils.randomAlphanumeric(20,20))))	
 

	val httpProtocol = http
		.baseUrl("http://mscustomers-ktl:8080")
	

  val headers_0 = Map(
    "Authorization" -> "Bearer eyJhbGciOiJIUzI1NiJ9.eyJjbGllbnRfaWQiOiJmb29BcHAifQ.Zdc_wPWaiqGE3nCeNtHSl_hQmSj6T4LU21Eu-Fu4yWM",
    "Content-Type" -> "application/json",
    "X-Api-Force-Sync" -> "false")


	val scn_customers_create = scenario("customer-create")
  	  .feed(feederCorrelationId02)
		  .exec(http("request_0")
			      .post("/customers")
			      .headers(headers_0)
            .header("X-Correlation-Id", "${uuid}")
            .body(RawFileBody("templates/customer_create.json"))
            .asJson
            .check(status is 202)
        )


  val accountStressAll = scenario("customers")
    .exec(scn_customers_create)

    //ok
  	//setUp(accountStressAll.inject(rampUsersPerSec(150).to(200).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(rampUsersPerSec(180).to(220).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(rampUsersPerSec(200).to(220).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(rampUsersPerSec(220).to(250).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(rampUsersPerSec(250).to(300).during(60.seconds))).protocols(httpProtocol)
    setUp(accountStressAll.inject(rampUsersPerSec(300).to(320).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(rampUsersPerSec(320).to(350).during(60.seconds))).protocols(httpProtocol)
    //KO
    //setUp(accountStressAll.inject(rampUsersPerSec(200).to(380).during(60.seconds))).protocols(httpProtocol)

    //setUp(accountStressAll.inject(rampUsersPerSec(150).to(200).during(60.seconds))).protocols(httpProtocol)
    //setUp(accountStressAll.inject(heavisideUsers(2000).during(60.seconds))).protocols(httpProtocol)
    

	//setUp(scn.inject(atOnceUsers(1))).protocols(httpProtocol)

	//setUp(scn.inject(rampUsersPerSec(50).to(100).during(10.seconds))).protocols(httpProtocol)

	//setUp(scn.inject(constantUsersPerSec(50).during(10.seconds))).protocols(httpProtocol)

	//setUp(scn.inject(constantUsersPerSec(385).during(10.seconds))).protocols(httpProtocol)

	//setUp(scn.inject(heavisideUsers(3850).during(10.seconds))).protocols(httpProtocol)
    //setUp(scn.inject(heavisideUsers(3850).during(10.seconds))).protocols(httpProtocol)
     //setUp(scn.inject(rampUsersPerSec(50).to(80).during(60.seconds))).protocols(httpProtocol)
    //- 5000
    // HPA 2 - 8 / 60%cpu || 60memory -- INCR

	//setUp(scn.inject(rampUsersPerSec(30000).to(385501).during(10.seconds))).protocols(httpProtocol)

    //setUp.inject(rampUsersPerSec(50).to(80).during(60.seconds)).protocols(httpConf02) 



/********************************************************************************************************

Modelo cerrados, donde controlas el número concurrente de usuarios.
Los sistemas cerrados son sistemas en los que se limita el número de usuarios simultáneos. A plena capacidad, un nuevo usuario puede ingresar de manera efectiva al sistema solo una vez que otro salga.
Ejmplo son el centro de llamadas donde todos los operadores están ocupados,
sitios web de emisión de tickets donde los usuarios se colocan en una cola cuando el sistema está a plena capacidad

Modelo abiertos, donde controlas la tasa de llegada de usuarios.
No tienen control sobre el número de usuarios concurrentes: los usuarios siguen llegando a pesar de que las aplicaciones tienen problemas para atenderlos. La mayoría de los sitios web se comportan de esta manera.

Modelo abierto
--------------

setUp(
  scn.inject(
    nothingFor(4.seconds), // 1
    atOnceUsers(10), // 2
    rampUsers(10).during(5.seconds), // 3
    constantUsersPerSec(20).during(15.seconds), // 4
    constantUsersPerSec(20).during(15.seconds).randomized, // 5
    rampUsersPerSec(10).to(20).during(10.minutes), // 6
    rampUsersPerSec(10).to(20).during(10.minutes).randomized, // 7
    heavisideUsers(1000).during(20.seconds) // 8
  ).protocols(httpProtocol)
)
Los componentes básicos para la inyección de perfiles de la forma que desee son:

1. nothingFor(duration): Pausa durante un tiempo determinado.
2. atOnceUsers(nbUsers): Inyecta un número determinado de usuarios a la vez.
3. rampUsers(nbUsers) during(duration): Inyecta un número determinado de usuarios distribuidos uniformemente en una ventana de tiempo de una duración determinada.
4. constantUsersPerSec(rate) during(duration): Inyecta usuarios a una velocidad constante, definida en usuarios por segundo, durante una duración determinada. Los usuarios se inyectarán a intervalos regulares.
5. constantUsersPerSec(rate) during(duration) randomized: Inyecta usuarios a una velocidad constante, definida en usuarios por segundo, durante una duración determinada. Los usuarios serán inyectados a intervalos aleatorios.
6. rampUsersPerSec(rate1) to (rate2) during(duration): Inyecta a los usuarios desde la tasa inicial hasta la tasa objetivo, definida en usuarios por segundo, durante una duración determinada. Los usuarios se inyectarán a intervalos regulares.
7. rampUsersPerSec(rate1) to(rate2) during(duration) randomized: Inyecta a los usuarios desde la tasa inicial hasta la tasa objetivo, definida en usuarios por segundo, durante una duración determinada. Los usuarios serán inyectados a intervalos aleatorios.
8. heavisideUsers(nbUsers) during(duration): Inyecta un número determinado de usuarios siguiendo una aproximación suave de la función escalón heaviside estirada a una duración determinada.


Modelo cerrado
--------------

setUp(
  scn.inject(
    constantConcurrentUsers(10).during(10.seconds), // 1
    rampConcurrentUsers(10).to(20).during(10.seconds) // 2
  )
)
1. constantConcurrentUsers(nbUsers) during(duration): Inyecte para que el número de usuarios simultáneos en el sistema sea constante
2. rampConcurrentUsers(fromNbUsers) to(toNbUsers) during(duration): Inyecte para que el número de usuarios simultáneos en el sistema aumente linealmente de un número a otro

*/

}
