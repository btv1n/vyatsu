package afisha.kukla;

import java.io.IOException;
import java.util.Date;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.regex.Pattern;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;

import afisha.structs.Play;
import parser.Parser;
import parser.general.PageRequestSettings;
import parser.general.RequestMethod;
import printer.Printer;

public class KuklaParser implements Parser<List<Play>> {
    private final KuklaPages pages;
    private final Map<String, String> monthNames;

    public KuklaParser(KuklaPages pages) {
        this.pages = pages;
        monthNames = new HashMap<>();

    }

    @Override
    public List<Play> Parse(Document doc, PageRequestSettings settings) {
        if (pages.getPagesCount() == 1) {
            var s = doc.select(".swiper-month .swiper-slide");
            for (Element slide : s) {
                var month_id = slide.attr("data-month");
                var month_name = slide.ownText();
                var data = "resource=239&id_res=" + month_id + "&ajaxchunk_name=spectacles&timestamp="
                        + Long.toString(new Date().getTime());
                pages.addPage(new PageRequestSettings(KuklaPages.AJAX_CHUNK_URL, RequestMethod.POST, data));
                monthNames.put(month_id, month_name);
            }
            return null;
        }
        List<Play> list = new LinkedList<>();

        var matcher = Pattern
                .compile("id_res=\\d+")
                .matcher(settings.getRequestBody());
        matcher.find();
        var month_id = matcher.group().substring(7);
        var month = monthNames.get(month_id);

        for (Element play : doc.select(".spec-item")) {
            var title = play.selectFirst(".spec-item__center .name").text();
            var url = KuklaPages.BASE_URL + play.selectFirst(".spec-item__center .name a").attr("href");
            var date = month + " " + play.selectFirst(".spec-item__left").text();
            var ageRestriction = play.selectFirst(".spec-age").ownText();
            var duration = "";
            var posterImageUrl = "";

            try {
                Document playPage = Jsoup.connect(url).get();
                String text = playPage.selectFirst(".content .container").text();
                int d = text.indexOf("Продолжительность спектакля - ");
                if (d > 0)
                    duration = text.substring(d + 30, text.indexOf('.', d));

                posterImageUrl = playPage.selectFirst(".spec-image").absUrl("src");
            } catch (IOException e) {
                Printer.printLnErr("Error while loading " + url);
            }

            list.add(new Play(title, date, duration, ageRestriction, posterImageUrl));
        }

        return list;
    }
}
