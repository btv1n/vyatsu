package article;

import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import com.lowagie.text.Document;
import com.lowagie.text.Font;
import com.lowagie.text.Paragraph;
import com.lowagie.text.Image;

import lombok.Getter;
import services.json.Jsonable;
import services.pdf.Pdfable;

public class Article implements Jsonable, Pdfable {

    private static SimpleDateFormat dateParser = new SimpleDateFormat("yyyy-MM-dd, HH:mm");

    private @Getter String author;
    private @Getter String title;
    private @Getter Date pubDate;
    private @Getter String[] tags;
    private @Getter String views;
    private @Getter String readingTime;
    private @Getter String imageUrl;
    private @Getter String preHeader;

    public Article(String content) throws ParseException {
        var lines = content.split("\\r?\\n");

        if (lines.length < 8)
            throw new ParseException("Content must contain at least 8 lines", 0);

        author = lines[0];
        title = lines[1];
        pubDate = dateParser.parse(lines[2]);
        readingTime = lines[3];
        views = lines[4];
        tags = lines[5].split(", ");
        imageUrl = lines[6];

        preHeader = "";
        for (int i = 7; i < lines.length; i++) {
            preHeader += lines[i];
            if (i != lines.length - 1)
                preHeader += "\n";
        }
    }

    @SuppressWarnings("unchecked")
    public JSONObject toJsonObject() {
        JSONObject obj = new JSONObject();
        obj.put("author", author);
        obj.put("title", title);
        obj.put("pubDate", pubDate.getTime());

        obj.put("views", views);
        obj.put("readingTime", readingTime);
        obj.put("imageUrl", imageUrl);
        obj.put("preHeader", preHeader);

        JSONArray jsonTags = new JSONArray();
        for (String tag : tags) {
            jsonTags.add(tag);
        }

        obj.put("tags", jsonTags);

        return obj;
    }

    public void toPdf(Document doc) {
        if (!doc.newPage())
            return;

        doc.addTitle(title);
        doc.addAuthor(author);

        doc.add(new Paragraph(title, new Font(Font.HELVETICA, 18, Font.BOLDITALIC)));
        doc.add(new Paragraph("Автор: " + author));
        doc.add(new Paragraph(pubDate.toString()));
        doc.add(new Paragraph("Просмотров: " + views));
        doc.add(new Paragraph("Время прочтения: " + readingTime));

        try {
            doc.add(Image.getInstance(imageUrl));
        } catch (IOException e) {
        }
        doc.add(new Paragraph(preHeader));
    }
}
