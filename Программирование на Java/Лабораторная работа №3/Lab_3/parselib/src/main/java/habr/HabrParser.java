package habr;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.time.ZoneOffset;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.jsoup.Connection.Response;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import habr.structs.Article;
import parser.Parser;
import parser.general.PageRequestSettings;
import printer.AnsiColor;
import printer.Printer;

public class HabrParser implements Parser<List<Article>> {
    @Override
    public List<Article> Parse(Document doc, PageRequestSettings settings) {
        Elements articlesList = doc.selectFirst(".tm-articles-list").select("article");
        List<Article> list = new ArrayList<>(articlesList.size());
        for (Element articleElement : articlesList) {
            Article article = parseArticle(articleElement);

            try {
                saveImage(article);
            } catch (Exception e) {
            }
            list.add(article);
        }
        return list;
    }

    private Article parseArticle(Element article) {
        Element titleElement = article.selectFirst("h2 a.tm-title__link");
        String title = "";
        if (titleElement != null) {
            title = titleElement.text();
        } else {
            Printer.printLnErr("Article title not found");
        }

        Element authorElement = article.selectFirst(".tm-user-info__username");
        String author = "";
        if (authorElement != null) {
            author = authorElement.text();
        } else {
            Printer.printLnErr("Article author not found");
        }

        Element readingTimeElement = article.selectFirst(".tm-article-reading-time__label");
        String readingTime = "";
        if (readingTimeElement != null) {
            readingTime = readingTimeElement.text();
        } else {
            Printer.printLnErr("Article reading time not found");
        }

        Element viewsElement = article.selectFirst(".tm-icon-counter span.tm-icon-counter__value");
        String views = "";
        if (viewsElement != null) {
            views = viewsElement.text();
        } else {
            Printer.printLnErr("Article views not found");
        }

        var bodyElement = article.selectFirst(".tm-article-body .article-formatted-body");
        String preview = "";
        String previewImageUrl = "";
        if (bodyElement != null) {
            var previewImage = bodyElement.selectFirst("img");
            if (previewImage != null) {
                previewImageUrl = previewImage.absUrl("src");
            }

            preview = bodyElement.text();

        } else {
            Printer.printLnErr("Article body not found");
        }

        var imageElement = article.selectFirst("img.tm-article-snippet__lead-image");
        if (imageElement != null) {
            previewImageUrl = imageElement.absUrl("src");
        }

        var hubsElements = article.select(".tm-publication-hubs .tm-publication-hub__link-container");
        String[] hubs = new String[hubsElements.size()];
        for (int i = 0; i < hubs.length; i++) {
            hubs[i] = hubsElements.get(i).text();
        }

        Element timeElement = article.selectFirst(".tm-article-datetime-published time");
        LocalDateTime date = null;

        if (timeElement != null) {
            try {
                date = LocalDateTime.parse(timeElement.attr("datetime").replace(".000Z", ""));
            } catch (Exception e) {
                Printer.printLnErr("Article date parse error");
            }
        } else {
            Printer.printLnErr("Article date not found");
        }
        Date d = null;
        if (date != null) {
            d = Date.from(date.toInstant(ZoneOffset.UTC));
        }

        return new Article(title,
                d,
                author,
                readingTime,
                views,
                hubs,
                preview,
                previewImageUrl);
    }

    private void saveImage(Article article) throws IOException {
        if (article.getPreviewImageUrl().length() == 0)
            return;

        var split = article.getPreviewImageUrl().split("/");
        var image_path = Paths.get("./images/" + split[split.length - 1]);
        Printer.printLn(image_path.toString(), AnsiColor.PURPLE);
        Files.createFile(image_path);
        Response resultImageResponse = Jsoup.connect(article.getPreviewImageUrl()).ignoreContentType(true).execute();

        FileOutputStream out = new FileOutputStream(new File(image_path.toString()));
        out.write(resultImageResponse.bodyAsBytes());
        out.close();
    }
}
