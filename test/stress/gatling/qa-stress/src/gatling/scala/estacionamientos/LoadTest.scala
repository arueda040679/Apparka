package estacionamientos

import io.gatling.core.Predef._
import io.gatling.core.structure.ScenarioBuilder
import io.gatling.http.Predef._
import io.gatling.http.protocol.HttpProtocolBuilder
import io.gatling.http.request.builder.HttpRequestBuilder.toActionBuilder

import scala.concurrent.duration._
import java.time.format.DateTimeFormatter
import scala.util.Random
import org.apache.commons.lang3.RandomStringUtils

class LoadTest extends Simulation {
    
    val httpConf02 = http.baseUrl("https://devjockey.azurewebsites.net/api")
    
    val feeder =  Iterator.continually(Map(
        "token" -> ( RandomStringUtils.randomAlphanumeric(10,20)),
        "estacionamientoID" -> Random.nextInt(20),
        "userId"->Random.nextInt(30),
        "imei" -> (Random.alphanumeric.take(20).mkString),
        "name" -> ( RandomStringUtils.randomAlphanumeric(10,20)),
        "lastName" -> ( RandomStringUtils.randomAlphanumeric(10,20)),
        "motherName" -> ( RandomStringUtils.randomAlphanumeric(10,20)),
        "documentNumer"-> ( RandomStringUtils.randomNumeric(8,8)),
        "phoneNumber"-> ( RandomStringUtils.randomNumeric(9,9)),
        "mail"-> ( RandomStringUtils.randomAlphanumeric(5,13))
        ))

    val fixedFeeder = Array(
        Map("so" -> "android"),
        Map("so" -> "ios")
        ).circular

    val headers = Map(
        "Content-Type" -> "application/json",
        "Accept" -> "application/json",
        "Authorization" -> "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiI5ZDkxZjRmZi05Yjc0LTRkYzUtYjFiNC1mMjg4YTllOTIyMDUiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vZTc4ZDlmMWQtNjM2OS00M2ZiLTkwY2MtMzQxNWRiOTIyMTI1L3YyLjAiLCJpYXQiOjE2NjQ1MTAwMjksIm5iZiI6MTY2NDUxMDAyOSwiZXhwIjoxNjY0NTEzOTI5LCJhaW8iOiJFMlpnWUZqRTlJbzF2OU1sdnNGRmFYSEh4cmQ5QUE9PSIsImF6cCI6IjlkOTFmNGZmLTliNzQtNGRjNS1iMWI0LWYyODhhOWU5MjIwNSIsImF6cGFjciI6IjEiLCJvaWQiOiI3ZjVhMjlhOC0yNTA1LTQzODUtYjMxYS0zOGYzMmVjOWJkNDQiLCJyaCI6IjAuQVJVQUhaLU41MmxqLTBPUXpEUVYyNUloSmZfMGtaMTBtOFZOc2JUeWlLbnBJZ1dLQUFBLiIsInJvbGVzIjpbImFwcC5yZWFkIiwiYXBwLndyaXRlIiwiaWRDb21lcmNpbzo5OTkwOTIiXSwic3ViIjoiN2Y1YTI5YTgtMjUwNS00Mzg1LWIzMWEtMzhmMzJlYzliZDQ0IiwidGlkIjoiZTc4ZDlmMWQtNjM2OS00M2ZiLTkwY2MtMzQxNWRiOTIyMTI1IiwidXRpIjoiMG9TNDh0U1I1VUNtTEh0WHNUNENBQSIsInZlciI6IjIuMCJ9.rCE2WbQ_NpRd23pdJnnFbMRtFdr8kZDQQn-dyuO5_x-ptTKuUc0I1YhGuPIt-m-ONXqVb7GXDuS3ytKJSAmacQpp9d5HiIYCm-il9mYO-DLjyaLaaRIoot9NXwLUhzC2xEpMTvH-j686zk6CQ0kQds7uJnFVESBbYdKSg5TXPmDhdVXw_4hf5MW0b6LpEC20V7kD6KxKyZIn5wZoVOgEevF2zZGywvIszzjdO8Nlgt3qXpUi9f-jJZ0ADd_UPSaxf6dLiasorfO1ZaKliToupaDNCu_EebOgCIjKUB2oBr3gX9zmGN6klrMaolBmpoAl6d4IUenPGq6e1mkLEhvvtg"
    )

     val scn_getEstacionamiento = scenario("get-scenario")
         .feed(feeder)
         .feed(fixedFeeder.random)
        .exec(
            http("get-estacionamiento")
                .get("/Estacionamiento/getEstacionamiento/${estacionamientoID}")
                .headers(headers)
                .asJson
                .check(status is 200)
            )
        .pause(0)


   val scn_token_create = scenario("token-create")
          .feed(feeder)
          .feed(fixedFeeder.random)
		  .exec(
            http("post-createToken")
			      .post("/usuario/setTokenRegistrar")
			      .headers(headers)
            .body(ElFileBody("templates/create_token.json"))
            .asJson
            .check(status is 200)
            )
        .pause(0)

     val scn_user_create = scenario("user-create")
          .feed(feeder)
          .feed(fixedFeeder.random)
		  .exec(
            http("post-setUsuario")
			      .post("/Usuario/setUsuario")
			      .headers(headers)
            .body(ElFileBody("templates/create_user.json"))
            .asJson
            .check(status is 200)
            )
        .pause(0)

    

    setUp(
      scn_getEstacionamiento.inject(heavisideUsers(200).during(20.seconds)).protocols(httpConf02),
      scn_token_create.inject(heavisideUsers(200).during(20.seconds)).protocols(httpConf02),
      scn_user_create.inject(heavisideUsers(200).during(20.seconds)).protocols(httpConf02)
    )

}