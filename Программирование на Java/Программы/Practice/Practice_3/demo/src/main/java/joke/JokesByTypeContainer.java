package joke;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import lombok.Getter;

// В этой структуре должны храниться данные следующего вида: тип шуток - список шуток данного типа
public class JokesByTypeContainer {
    @Getter
    private String type;
    private Map<String, List<JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline>> jokes = new HashMap<>();

    public JokesByTypeContainer() {
    }

    public void add(Joke joke) throws IllegalArgumentException {
        add(new JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline(joke), joke.getType());
    }

    public void add(JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline joke, String type) {
        if (!jokes.containsKey(type))
            jokes.put(type, new LinkedList<>());
        jokes.get(type).add(joke);
    }

    public List<JokeWithoutTypeAndUnreasonablyCombinedSetupAndPunchline> getJokesByType(String type) {
        return jokes.get(type);
    }

    public Set<String> getTypes() {
        return jokes.keySet();
    }
}
