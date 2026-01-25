package structs;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

@AllArgsConstructor
@ToString
public class OnlineShop {
    @Getter
    private String name;
    @Getter
    private int avgScore;
    @Getter
    private Review[] reviews;
}
