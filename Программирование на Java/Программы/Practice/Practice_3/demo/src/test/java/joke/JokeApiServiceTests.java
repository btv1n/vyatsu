package joke;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

import org.junit.jupiter.api.Test;

public class JokeApiServiceTests {
    private final JokeApiService jokeService = new JokeApiService();

    @Test
    void randomJoke() {
        Joke joke = assertDoesNotThrow(() -> jokeService.getRandomJoke());
        assertNotNull(joke);
    }
    @Test
    void randomJokes() {
        int count = 100;
        Joke[] jokes = assertDoesNotThrow(() -> jokeService.getRandomJokes(count));
        assertNotNull(jokes);
        assertEquals(count, jokes.length);
        for (Joke joke : jokes) {
            assertNotNull(joke);
        }
    }
    @Test
    void getRandomJokeOfType() {
        String type = "programming";
        Joke joke = assertDoesNotThrow(() -> jokeService.getRandomJoke(type));
        assertNotNull(joke);
        assertEquals(type, joke.getType());
    }
    @Test
    void getTenJokesOfType() {
        String type = "programming";
        Joke[] jokes = assertDoesNotThrow(() -> jokeService.getTenJokes(type));
        assertNotNull(jokes);
        assertEquals(10, jokes.length);
        for (Joke joke : jokes) {
            assertNotNull(joke);
            assertEquals(type, joke.getType());
        }
    }
}
