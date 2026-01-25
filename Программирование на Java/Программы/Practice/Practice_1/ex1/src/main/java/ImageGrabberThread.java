
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.jsoup.Jsoup;
import org.jsoup.Connection.Response;

import com.google.gson.Gson;

import structs.Review;

class ImageGrabberThread extends Thread {
    private String json_path;
    private String save_path;

    ImageGrabberThread(String json_path, String save_path) {
        this.json_path = json_path;
        this.save_path = save_path;
    }

    @Override
    public void run() {
        try {
            var reviews = readReviews();
            for (Review review : reviews) {
                var url = review.getBook().getCoverUrl();
                var url_split = review.getBook().getCoverUrl().split("/");
                String image_name = url_split[url_split.length - 1];
                var image_path = Paths.get(save_path + image_name);
                if (!Files.exists(image_path)) {
                    try {
                        System.out.println("Downloading: " + image_name);
                        saveImage(url, image_path);
                    } catch (Exception e) {
                        System.err.println(e.getMessage());
                    }
                } else {
                    //System.out.println("Already downloaded: " + image_name);
                }
            }
        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }

    private Review[] readReviews() throws IOException {
        var gson = new Gson();
        var content = Files.readString(Paths.get(json_path));
        return gson.fromJson(content, Review[].class);
    }

    private void saveImage(String url, Path image_path) throws FileNotFoundException, IOException {
        Files.createFile(image_path);
        Response resultImageResponse = Jsoup.connect(url).ignoreContentType(true).execute();

        FileOutputStream out = new FileOutputStream(new java.io.File(image_path.toString()));
        out.write(resultImageResponse.bodyAsBytes());
        out.close();
    }
}