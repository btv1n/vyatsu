package labs.teatr;

import labs.IXMLable;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;


@ToString
@AllArgsConstructor
@Getter
public class PlayWithDates implements IXMLable {
    private String title;
    private List<Date> dates;
    private Integer duration;
    private Integer ageRestriction;
    private String posterImageUrl;

    @Override
    public String toXml() {
        StringBuilder sb = new StringBuilder();
        sb.append("<play ")
                .append("duration=\"").append(duration).append("\" ")
                .append("ageRestriction=\"").append(ageRestriction).append("\"")
                .append(">");

        sb.append("<title>").append(title).append("</title>");
        sb.append("<posterImage>").append(posterImageUrl).append("</posterImage>");

        sb.append("<dates>");
        for (Date d: dates) {
            sb.append("<date>").append(d).append("</date>");
        }
        sb.append("</dates>");

        sb.append("</play>");
        return sb.toString();
    }
}
