package afisha.structs;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;


@ToString
@AllArgsConstructor
@Getter
public class Play {
    private String title;
    private String date;
    private String duration;
    private String ageRestriction;
    private String posterImageUrl;
}
