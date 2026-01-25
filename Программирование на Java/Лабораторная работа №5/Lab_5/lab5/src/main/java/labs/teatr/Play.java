package labs.teatr;

import labs.IXMLable;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.ToString;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;


@ToString
@AllArgsConstructor
@Getter
public class Play implements IXMLable {
    private String title;
    private String date;
    private Integer duration;
    private Integer ageRestriction;
    private String posterImageUrl;

    public String getDateString() {
        return date;
    }

    public Date getDate() {
        Locale locale = new Locale("ru");
        String[] formats = {"dd MMMM HH:mm", "dd LLLL yyyy HH:mm", "dd LLLL HH:mm"};
        Date date = null;
        for (var format : formats) {
            var parser = new SimpleDateFormat(format, locale);
            try {
                date = parser.parse(this.date);
            } catch (ParseException e) {
            }
        }
        if (date != null) {
            date.setYear(124);
        }
        return date;
    }

    public Calendar getDateCalendar()
    {
        Calendar result = Calendar.getInstance();
        result.setTime(this.getDate());
        return result;
    }

    @Override
    public String toXml() {
        StringBuilder sb = new StringBuilder();
        sb.append("<play ")
                .append("date=\"").append(getDate()).append("\" ")
                .append("duration=\"").append(duration).append("\" ")
                .append("ageRestriction=\"").append(ageRestriction).append("\"")
                .append(">");

        sb.append("<title>").append(title).append("</title>");
        sb.append("<posterImage>").append(posterImageUrl).append("</posterImage>");

        sb.append("</play>");
        return sb.toString();
    }
}
