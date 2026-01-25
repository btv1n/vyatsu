package joke;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpRequest.Builder;
import java.time.Duration;

import com.google.gson.Gson;

import lombok.var;

public class JokeApiService {
    private URI uri;
    private HttpClient httpClient = HttpClient.newHttpClient();
    private Builder httpRequestBuilder;
    private Gson gson;

    public JokeApiService() {
        this("https://official-joke-api.appspot.com/");
    }

    public JokeApiService(String uri) {
        gson = new Gson();
        this.uri = URI.create(uri);
        httpClient = HttpClient.newHttpClient();
        this.httpRequestBuilder = HttpRequest
                .newBuilder()
                .timeout(Duration.ofSeconds(15));
    }

    public Joke getRandomJoke() throws IOException, InterruptedException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("/jokes/random"))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return gson.fromJson(response.body(), Joke.class);
    }

    public Joke[] getRandomJokes(int count) throws IOException, InterruptedException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("/jokes/random/" + Integer.toString(count)))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return gson.fromJson(response.body(), Joke[].class);
    }

    public Joke getRandomJoke(String type) throws IOException, InterruptedException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("/jokes/" + type + "/random"))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return gson.fromJson(response.body(), Joke[].class)[0];
    }

    public Joke[] getTenJokes(String type) throws IOException, InterruptedException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("/jokes/" + type + "/ten"))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return gson.fromJson(response.body(), Joke[].class);
    }

    public String[] getJokeTypes() throws IOException, InterruptedException {
        var request = httpRequestBuilder.uri(this.uri.resolve("/types")).GET().build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return gson.fromJson(response.body(), String[].class);
    }
}
