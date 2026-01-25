package core;

import core.habr.HabrParser;
import core.habr.HabrSettings;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

import java.io.IOException;

public class HtmlLoader {

    String url;

    public HtmlLoader(ParserSettings settings)
    {
        url = HabrSettings.BASE_URL+"/"+HabrSettings.PREFIX;//"{settings.BaseUrl}/{settings.Prefix}/";
    }

    public Document GetSourceByPageId(int id) throws IOException {
        String currentUrl = url.replace("{CurrentId}", Integer.toString(id));
        return Jsoup.connect(currentUrl).get();
    }
}
