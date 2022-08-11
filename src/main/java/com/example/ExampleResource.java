package com.example;

import io.quarkus.logging.Log;

import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("/log")
public class ExampleResource {

    @POST
    @Produces(MediaType.TEXT_PLAIN)
    public void hello(String body) {
        log(body);
    }

    private static void log(String msg) {
        String toLog =
                "\n=========================================================================" +
                "\n=                               LOG                                     =" +
                "\n=========================================================================" +
                "\n" + msg +
                "\n=========================================================================";

        Log.info(toLog);
    }
}
