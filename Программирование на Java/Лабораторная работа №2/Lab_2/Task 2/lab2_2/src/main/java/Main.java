import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.lowagie.text.Document;
import com.lowagie.text.pdf.PdfWriter;

import article.Article;
import services.json.JacksonService;
import services.json.SimpleJSONService;

public class Main {
    private static String articlesPath = "resources/articles.txt";

    public static void main(String[] args) {
        Path f = Paths.get(articlesPath);
        String s = null;
        try {
            s = Files.readString(f);
        } catch (IOException e) {
            System.err.println(e.getMessage());
            return;
        }

        var articlesContent = s.split("\\r?\\n\\r?\\n");
        var articles = new Article[articlesContent.length];
        for (int i = 0; i < articles.length; i++) {
            try {
                articles[i] = new Article(articlesContent[i]);
            } catch (ParseException e) {
                System.err.println(e);
            }
        }

        try {
            String json = JacksonService.toJsonString(articles);
            Files.write(Paths.get("jackson.json"), json.getBytes());
        } catch (JsonProcessingException e) {
        } catch (IOException e) {
        }

        try {
            String json = SimpleJSONService.toJsonString(articles);
            Files.write(Paths.get("simple-json.json"), json.getBytes());
        } catch (JsonProcessingException e) {
        } catch (IOException e) {
        }

        Document doc = new Document();
        try {
            PdfWriter.getInstance(doc, new FileOutputStream("1.pdf"));
            doc.open();
            articles[0].toPdf(doc);
        } catch (Exception e) {
            // TODO: handle exception
        } finally {
            doc.close();
        }
    }
}
