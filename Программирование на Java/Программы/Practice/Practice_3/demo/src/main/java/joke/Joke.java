package joke;

import lombok.Data;

@Data
public class Joke {
    private String type;
    private String setup;
    private String punchline;
    private Integer id;
}
