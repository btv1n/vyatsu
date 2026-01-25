package com.example;

import java.io.IOException;

import javax.xml.parsers.ParserConfigurationException;

import org.xml.sax.SAXException;

import joke.Joke;
import joke.JokeApiService;
import joke.JokesByTypeContainer;
import lombok.var;
import user.UserApiService;

public final class App {
    public static void main(String[] args)
            throws IOException, InterruptedException, ParserConfigurationException, SAXException {
        getJokes();
        System.out.println("\n\n");
        //getUsers();
    }

    private static void getJokes() throws IOException, InterruptedException {
        JokeApiService jokeService = new JokeApiService();
        JokesByTypeContainer jokes = new JokesByTypeContainer();
        for (Joke joke : jokeService.getRandomJokes(451)) {
            jokes.add(joke);
        }

        System.out.println("Jokes by type:");
        for (String type : jokes.getTypes()) {
            System.out.println(type + ":");
            for (var jokesByType : jokes.getJokesByType(type)) {
                System.out.println(jokesByType);
            }
        }
    }

    private static void getUsers()
            throws IOException, InterruptedException, ParserConfigurationException, SAXException {
        System.out.println("Users from JSON:");
        UserApiService userApiService = new UserApiService();
        var usersJson = userApiService.getUsersJson(10);
        for (var user : usersJson) {
            System.out.println(user);
        }
        System.out.println("Users from XML:");
        var usersXml = userApiService.getUsersXml(10);
        for (var user : usersXml) {
            System.out.println(user);
        }
    }
}
