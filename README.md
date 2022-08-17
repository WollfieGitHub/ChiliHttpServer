# HTTP Server Log Output

## Steps to log the Output of the Occulus onto this server :

1. Connect both the computer running the server and the occulus on a local network created by a mobile device (This acts as an intermediate network before the EPFL one or mobile data)
2. Go into your terminal and type `ipconfig`. Scroll to the "Wireless LAN adapter Wi-fi" header and search for the IPV4 field.
3. Copy the IPV4 field value mentioned above in : 
  1. The `quarkus.http.host` of the `application.properties` file located in `src/main/resources/` folder
  2. The url var in the `ConsoleToGUI.cs` file located in `scripts/` folder such that the var is in the format `ws://<IPV4 address>:8080/log/...`
4. Copy the `ConsoleToGUI.cs` script into your unity project and add an object with this script to your game
5. Go to the root of the server's project with your terminal and type `mvnw package`
6. Once it is done, navigate into the `target/` folder and type `java -jar <filename.jar>`
7. Run the unity project into the headset and observe the logged messages on the console
