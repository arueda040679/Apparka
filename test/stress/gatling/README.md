# STRESS

## Load test

1. nothingFor(duration): Pause for a given duration.
1. atOnceUsers(nbUsers): Injects a given number of users at once.
1. rampUsers(nbUsers) during(duration): Injects a given number of users distributed evenly on a time window of a given duration.
1. constantUsersPerSec(rate) during(duration): Injects users at a constant rate, defined in users per second, during a given duration. Users will be injected at regular intervals.
1. constantUsersPerSec(rate) during(duration) randomized: Injects users at a constant rate, defined in users per second, during a given duration. Users will be injected at randomized intervals.
1. rampUsersPerSec(rate1) to (rate2) during(duration): Injects users from starting rate to target rate, defined in users per second, during a given duration. Users will be injected at regular intervals.
1. rampUsersPerSec(rate1) to(rate2) during(duration) randomized: Injects users from starting rate to target rate, defined in users per second, during a given duration. Users will be injected at randomized intervals.
1. heavisideUsers(nbUsers) during(duration): Injects a given number of users following a smooth approximation of the heaviside step function stretched to a given duration.

    ```scala
    nothingFor(4.seconds), // 1
    atOnceUsers(10), // 2
    rampUsers(10).during(5.seconds), // 3
    constantUsersPerSec(20).during(15.seconds), // 4
    constantUsersPerSec(20).during(15.seconds).randomized, // 5
    rampUsersPerSec(10).to(20).during(10.minutes), // 6
    rampUsersPerSec(10).to(20).during(10.minutes).randomized, // 7
    heavisideUsers(1000).during(20.seconds) // 8
    ```