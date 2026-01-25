package afisha.kukla;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse.BodyHandlers;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

import com.google.gson.JsonParser;

import parser.general.PageLoader;
import parser.general.PageRequestSettings;

public class KuklaPageLoader implements PageLoader {
    private final HttpClient client = HttpClient.newHttpClient();

    public KuklaPageLoader() {

    }

    @Override
    public Document load(PageRequestSettings page) throws IOException {
        switch (page.getMethod()) {
            case GET -> {
                return Jsoup.connect(page.getUrl()).get();
            }
            case POST -> {
                try {
                    String json = post(page);
                    String html = JsonParser.parseString(json).getAsJsonObject().get("content").getAsString();
                    Document doc = Jsoup.parse(html);
                    return doc;
                } catch (Exception e) {
                    return null;
                }
            }
            default -> {
                return null;
            }
        }
    }

    private String post(PageRequestSettings page) throws IOException {
        HttpClient client = HttpClient.newHttpClient();
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(page.getUrl()))
                .header("Accept", "application/json, text/javascript, */*; q=0.01")
                .header("Cookie", "PHPSESSID=92776271951fcd3a1f7ae2f3204eeb65")
                .header("Origin", "https://kirovkukla.ru")
                .header("Referer", "https://kirovkukla.ru/afisha")
                .header("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:132.0) Gecko/20100101 Firefox/132.0")
                .header("X-Requested-With", "XMLHttpRequest")
                .header("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8")
                .POST(HttpRequest.BodyPublishers.ofString(page.getRequestBody()))
                .build();

        try {
            var res = client.send(request, BodyHandlers.ofString());
            return res.body();
        } catch (InterruptedException ex) {
        }

        return "";
    }
}
