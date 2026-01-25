package structs;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

@AllArgsConstructor
@ToString
public class Review {
    @Getter
    private String author;
    @Getter
    private int score;
    @Getter
    private String advantages;
    @Getter
    private String disadvantages;
    @Getter
    private String text;
}
