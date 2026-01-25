
import java.io.IOException;
import java.text.ParseException;
import java.util.LinkedList;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;

import lombok.var;

public final class App {
    private static final String url = "https://na-negative.ru/internet-magaziny";

    public static void main(String[] args) {
        try {
            grab(url);
        } catch (ParseException e) {
            System.err.println(e.getMessage());
        } catch (IOException e) {
            System.err.println(e.getMessage());
        } catch (InterruptedException e) {
            System.err.println(e.getMessage());
        }
    }

    private static void grab(String url) throws IOException, ParseException, InterruptedException {
        Document doc = Jsoup.connect(url).userAgent("Chrome/129.0.6668.71").get();
        var page_count = getPageCount(doc);
        var threads = new LinkedList<PageGrabberThread>();
        for (int i = 1; i <= page_count; i++) {
            var thread = new PageGrabberThread(url + "?page=" + i, "page_" + i + ".json");
            thread.start();
            threads.add(thread);
        }

        for (var thread : threads) {
            thread.join();
        }
    }

    private static int getPageCount(Document doc) throws IOException, ParseException {
        Element el = doc.selectFirst(".frame .pagination-holder>.pagination li:nth-last-child(2):not(.arrow) a");
        if (el == null)
            return 1;
        return Integer.parseInt(el.ownText());
    }
}
