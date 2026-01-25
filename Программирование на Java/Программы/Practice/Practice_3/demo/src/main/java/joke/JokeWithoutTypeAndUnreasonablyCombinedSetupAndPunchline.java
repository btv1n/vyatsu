package joke;

import lombok.Data;

@Data
public class JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline {
    private String text;
    private Integer id;

    public JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline(Joke joke) {
        this.text = joke.getSetup() + " " + joke.getPunchline();
        this.id = joke.getId();
    }
}
