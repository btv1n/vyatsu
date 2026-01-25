package labs.habr;

import java.util.Date;

import labs.IXMLable;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

@ToString
@AllArgsConstructor
@Getter
public class Article implements IXMLable {
    String title;
    Date date;
    String author;
    String readingTime;
    String views;
    String[] hubs;
    String preview;
    String previewImageUrl;

    public int getViewsCount() {
        try {
            if (views.contains("K")) {
                return (int) (Double.parseDouble(
                        views.substring(0, views.length() - 2)
                    ) *
                    1000);
            }
            return Integer.parseInt(views);
        } catch (Exception e) {
            return 0;
        }
    }

    public int getReadingTimeMinutes() {
        try {
            return Integer.parseInt(readingTime.split(" ")[0]);
        } catch (Exception e) {
            return 0;
        }
    }

    public String toXml()
    {
        StringBuilder sb = new StringBuilder();
        sb.append("<article ")
                .append("views=\"").append(views).append("\" ")
                .append("date=\"").append(date.toString()).append("\" ")
                .append("author=\"").append(author).append("\" ")
                .append("readingtime=\"").append(readingTime).append("\"")
                .append(">");

        sb.append("<hubs>");
        for (String hub : hubs)
            sb.append("<hub>").append(hub).append("</hub>");
        sb.append("</hubs>");

        sb.append("<title>").append(title).append("</title>");
        sb.append("<previewImage>").append(previewImageUrl).append("</previewImage>");
        sb.append("<preview>").append(preview).append("</preview>");

        sb.append("</article>");
        return sb.toString();
    }
}
