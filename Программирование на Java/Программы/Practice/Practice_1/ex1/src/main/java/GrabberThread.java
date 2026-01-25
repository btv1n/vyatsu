
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import org.jsoup.HttpStatusException;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import com.google.gson.Gson;

import ConsolePrinter.Color;
import ConsolePrinter.Printer;
import structs.Book;
import structs.Review;

class GrabberThread extends Thread {
    private static final String UA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:131.0) Gecko/20100101 Firefox/131.0";

    private final Map<String, Book> books; // url -> book
    private final int start_page;
    private final int end_page;
    private final String url;
    private final String path;

    GrabberThread(int start_page, int end_page, String url, String path, Map<String, Book> books) {
        this.start_page = start_page;
        this.end_page = end_page;
        this.url = url;
        this.path = path;
        this.books = books;
    }

    @Override
    public void run() {
        var reviews = new LinkedList<Review>();
        for (int i = start_page; i <= end_page; i++) {
            try {
                if (i == 1)
                    getPage(url, reviews);
                else
                    getPage(url + "page/" + i + "/", reviews);
                Printer.printLn("Processed page #" + i + " (" + end_page + ")", Color.ANSI_GREEN);
            } catch (IOException e) {
                Printer.printLnErr("Error on page #" + i + ": " + e.getMessage());
            }
        }

        try {
            var gson = new Gson();
            Files.write(Paths.get(path), gson.toJson(reviews).getBytes());
        } catch (IOException e) {
            Printer.printLnErr(e.getMessage());
        }
    }

    private List<Review> getPage(String url, List<Review> list) throws IOException {
        try {
            Document doc = GrabberThread.tryGet(url);
            if (doc == null)
                return list;
            return getPage(doc, list);
        } catch (HttpStatusException e) {
            Printer.printLnErr("Http error " + e.getStatusCode() + " " + url);
        }

        return list;
    }

    private List<Review> getPage(Document doc, List<Review> list) throws IOException {
        Elements comments = doc.select("#dle-comments-list>div:not(#dle-ajax-comments)");
        for (Element comment : comments) {
            list.add(getReview(comment.selectFirst(".comment")));
        }
        return list;
    }

    private Review getReview(Element comment) throws IOException {
        Element comment_text = comment.selectFirst(".comment-text");
        Element author_span = comment_text.selectFirst(".comment-header span[itemprop=author]");
        Element author_link = author_span.selectFirst("#commentator_nick");

        String author = author_span.ownText();
        if (author_link != null)
            author = author_link.ownText();

        String text = comment_text.selectFirst("span[itemprop=reviewBody]>div").ownText();

        return new Review(getBook(comment), author, text);
    }

    private Book getBook(Element comment) {
        var book_url = comment.selectFirst("h4 a").absUrl("href");
        Book book;

        synchronized (this.books) {
            if (books.containsKey(book_url)) {
                return books.get(book_url);
            }
            book = new Book("", "", "", 0f);
            books.put(book_url, book);
        }

        Document doc;
        try {
            doc = GrabberThread.tryGet(book_url);
            if (doc == null)
                return book;
        } catch (HttpStatusException e) {
            Printer.printLnErr("Http error " + e.getStatusCode() + " " + url);
            return book;
        }

        var title = doc.selectFirst("h1>[itemprop=name]").ownText();
        var author = doc.selectFirst("h1>[itemprop=author]").ownText();
        var cover_url = doc.selectFirst("img[itemprop=image]").absUrl("src");
        var score = doc.selectFirst(".rating[itemprop=aggregateRating]>.rating_digits>.rating_digits_1").ownText();

        book.setTitle(title);
        book.setAuthor(author);
        book.setCoverUrl(cover_url);
        book.setScore(Float.parseFloat(score));

        System.out.println(author + " - " + title);
        return book;
    }

    private static Document tryGet(String url) throws HttpStatusException {
        Document doc = null;
        try {
            // Если 503 то ждем какое-то время и пытаемся ещё максимум 10 раз
            for (int attempt = 0, timeout = 0; doc == null && attempt < 10; attempt++) {
                try {
                    sleep(timeout);
                    doc = Jsoup.connect(url).userAgent(UA).get();
                } catch (HttpStatusException e) {
                    switch (e.getStatusCode()) {
                        case 503:
                            timeout += 100;
                            break;
                        case 404:
                            Printer.printLnErr("Error 404: " + url);
                            return null;
                        default:
                            throw e;
                    }
                } catch (IOException e) {
                    timeout += 100;
                }
            }
        } catch (HttpStatusException e) {
            throw e;
        } catch (InterruptedException e) {
            Printer.printLnErr("Error while getting page: " + e.getMessage());
            Printer.printLnErr(url);
            return null;
        }

        return doc;
    }
}