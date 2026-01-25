package afisha.dramteatr;

import java.util.ArrayList;
import java.util.List;

import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import afisha.structs.Play;
import parser.Parser;
import parser.general.PageRequestSettings;

public class DramteatrParser implements Parser<List<Play>> {
    private final DramteatrPages pages;

    public DramteatrParser(DramteatrPages pages) {
        this.pages = pages;
    }

    @Override
    public List<Play> Parse(Document doc, PageRequestSettings settings) {
        Elements elements = doc.select("#list_afisha .t_afisha");
        List<Play> list = new ArrayList<>(elements.size());

        for (Element e : elements) {
            String title = e.selectFirst("h3>a").ownText();
            String date = e.selectFirst(".date_afisha").text();
            String duration = e.selectFirst("div:has(>.name_hrono)").ownText();
            String ageRestriction = e.selectFirst(".value_limit").text();
            String posterImage = e.selectFirst("img").absUrl("src");

            list.add(new Play(title, date, duration, ageRestriction, posterImage));
        }

        return list;
    }
}
