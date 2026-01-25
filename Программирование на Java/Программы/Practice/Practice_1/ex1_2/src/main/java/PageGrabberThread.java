import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.ParseException;
import java.util.LinkedList;
import java.util.List;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import com.google.gson.Gson;

import lombok.var;
import structs.OnlineShop;
import structs.Review;

class PageGrabberThread extends Thread {
    private String url;
    private String path;

    PageGrabberThread(String url, String path) {
        this.url = url;
        this.path = path;
    }

    @Override
    public void run() {
        try {
            var list = new LinkedList<OnlineShop>();
            var doc = Jsoup.connect(url).userAgent("Chrome/129.0.6668.71").get();
            
            parsePage(doc, list);

            var gson = new Gson();
            Files.write(Paths.get(path), gson.toJson(list).getBytes());
        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }

    private static int getPageCount(Document doc) throws IOException, ParseException {
        Element el = doc.selectFirst(".frame .pagination-holder>.pagination li:nth-last-child(2):not(.arrow) a");
        if (el == null)
            return 1;
        return Integer.parseInt(el.ownText());
    }

    private static void parsePage(Document doc, List<OnlineShop> list) throws IOException, ParseException {
        Elements shops = doc.select(".frame .reviewers-holder .find-list-block .find-list-box");
        for (Element shop : shops) {
            Element title_element = shop.selectFirst(">.ss");
            String reviews_link = title_element.absUrl("href");
            String name = title_element.ownText().substring(9);
            int avg_score = Integer.parseInt(shop.selectFirst(".sro").ownText().split(" — ")[1]);
            int review_count = Integer.parseInt(shop.selectFirst(".reviewers>.num").ownText());

            if (review_count < 50) {
                list.add(new OnlineShop(name, avg_score, parseReviewsPages(reviews_link)));
            }
        }
    }

    private static Review[] parseReviewsPages(String url) throws IOException, ParseException {
        int page_count = 1;
        var list = new LinkedList<Review>();

        for (int i = 1; i <= page_count; i++) {
            Document doc = Jsoup.connect(url + "?page=" + i).userAgent("Chrome/129.0.6668.71").get();
            parseReviews(doc, list);
            page_count = getPageCount(doc);
        }

        return list.toArray(new Review[list.size()]);
    }

    private static void parseReviews(Document doc, List<Review> list) {
        Elements reviews = doc.select(".frame .reviewers-block .reviewers-box[itemprop=review]");

        for (Element review : reviews) {
            Element name_element = review.selectFirst("header.head>.info>.name");
            String author = name_element.selectFirst("[itemprop=author]").ownText();
            int score = Integer
                    .parseInt(name_element.selectFirst("[itemprop=reviewRating]>[itemprop=ratingValue]").ownText());

            Element description_element = review.selectFirst("table[itemprop=description]");
            String advantages = description_element.selectFirst("[itemprop=pro]").ownText();
            String disadvantages = description_element.selectFirst("[itemprop=contra]").ownText();
            String text = description_element.selectFirst("[itemprop=reviewBody]").ownText();
            list.add(new Review(author, score, advantages, disadvantages, text));
        }
    }
}