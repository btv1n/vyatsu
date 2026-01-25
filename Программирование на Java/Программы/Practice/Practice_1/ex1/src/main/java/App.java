import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.ParseException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.stream.Collectors;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

import structs.Book;

public class App {
    private static final String URL = "https://iknigi.net/otzivi-na-knigi/";
    private static final String IMAGES_PATH = "images/";
    private static final String REVIEWS_PATH = "reviews/";

    public static void main(String[] args) {
        //getPages(URL, 12);

        try {
            System.out.println("Downloading images");
            getImages();
        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }

    private static void getImages() throws IOException {
        var threads = new LinkedList<ImageGrabberThread>();
        var paths = Files.list(Paths.get(REVIEWS_PATH))
                .map(Path::getFileName)
                .map(Path::toString)
                .collect(Collectors.toList());
        try {
            Files.createDirectories(Paths.get("images"));
            for (String path : paths) {
                ImageGrabberThread image_grabber_thread = new ImageGrabberThread(REVIEWS_PATH + path, IMAGES_PATH);
                image_grabber_thread.start();
                threads.add(image_grabber_thread);
            }

            for (var thread : threads) {
                thread.join();
            }
        } catch (IOException | InterruptedException e) {
            System.err.println(e.getMessage());
        }
    }

    private static void getPages(String url, int threadsNum) {
        Map<String, Book> books = new HashMap<>();

        try {
            var count = getPagesCount(url);
            System.out.println("Pages count: " + count);
            var threads = new LinkedList<GrabberThread>();
            Files.createDirectories(Paths.get(REVIEWS_PATH));

            var step = (int)Math.ceil(count / threadsNum);
            while (count > 0) {
                var start_page = count - Math.min(step, count) + 1;
                var end_page = count;
                var grabber_thread = new GrabberThread(
                    start_page,
                    end_page,
                    url,
                    REVIEWS_PATH + "reviews_" + start_page + "_" + end_page + ".json",
                    books
                );
                grabber_thread.start();
                threads.add(grabber_thread);
                count -= step;
            }
            

            for (var thread : threads) {
                thread.join();
            }

        } catch (IOException | InterruptedException | ParseException e) {
            System.err.println(e.getMessage());
        }
    }

    private static int getPagesCount(String url) throws ParseException, IOException {
        Document doc = Jsoup.connect(url).userAgent("Chrome/129.0.6668.71").get();
        int pages_count = Integer
                .parseInt(doc.selectFirst(".dle-comments-navigation .navigation .nav-center :last-child").ownText());
        return pages_count;
    }
}
