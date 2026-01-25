package habr.structs;

import java.util.Date;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

@ToString
@AllArgsConstructor
@Getter
public class Article {
    String title;
    Date date;
    String author;
    String readingTime;
    String views;
    String[] hubs;
    String preview;
    String previewImageUrl;
}
