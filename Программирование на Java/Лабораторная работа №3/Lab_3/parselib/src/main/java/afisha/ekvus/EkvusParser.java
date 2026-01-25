package afisha.ekvus;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import afisha.structs.Play;
import parser.Parser;
import parser.general.PageRequestSettings;

public class EkvusParser implements Parser<List<Play>> {
    @Override
    public List<Play> Parse(Document doc, PageRequestSettings settings) {
        Elements elements = doc.select("table.myafisha tr:not(:has(h2))");
        List<Play> list = new ArrayList<>(elements.size());

        try {
            Files.write(Paths.get("./evk.html"), doc.outerHtml().getBytes());
        } catch (Exception e) {
        }

        for (Element element : elements) {
            Elements cols = element.select("td");

            String title = cols.get(1).selectFirst("a").ownText();
            String date = cols.get(0).selectFirst("font").ownText();
            String ageRestriction = cols.get(1).selectFirst("a span.al, a span.al_s").ownText();
            String url = "https://ekvus-kirov.ru" + cols.get(1).selectFirst("a").attr("href");
            String duration = "";
            String posterImage = "";

            try {
                Document doc2 = Jsoup.connect(url).get();
                posterImage = doc2.selectFirst("img.img_right").absUrl("src");
            } catch (Exception e) {
            }

            list.add(new Play(title, date, duration, ageRestriction, posterImage));
        }

        return list;
    }
}
