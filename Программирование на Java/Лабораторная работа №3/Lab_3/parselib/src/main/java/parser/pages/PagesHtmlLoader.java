package parser.pages;

import java.io.IOException;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

public class PagesHtmlLoader {
    String url;

    public PagesHtmlLoader(PagesParserSettings settings)
    {
        url = settings.baseUrl + "/" + settings.suffix;
    }

    public Document load(int page) throws IOException {
        String currentUrl = url.replace("{page}", Integer.toString(page));
        return Jsoup.connect(currentUrl).get();
    }
}
