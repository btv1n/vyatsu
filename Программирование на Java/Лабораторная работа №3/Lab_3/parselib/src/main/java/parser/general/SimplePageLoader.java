package parser.general;

import java.io.IOException;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

public class SimplePageLoader implements PageLoader {
    @Override
    public Document load(PageRequestSettings page) throws IOException {
        switch (page.getMethod()) {
            case GET -> {
                return Jsoup.connect(page.getUrl()).get();
            }
            case POST -> {
                return Jsoup.connect(page.getUrl()).requestBody(page.getRequestBody()).post();
            }
            default -> {
                return null;
            }
        }
    }
}
