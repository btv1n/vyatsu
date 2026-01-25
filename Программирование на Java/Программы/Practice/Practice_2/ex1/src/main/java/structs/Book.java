package structs;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@AllArgsConstructor
@ToString
@Getter 
@Setter
public class Book {
    private String title;
    private String author;
    private String coverUrl;
    private float score;
}
