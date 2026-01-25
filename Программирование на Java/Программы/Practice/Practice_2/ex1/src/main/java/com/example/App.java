package com.example;

import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Arrays;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.function.Predicate;

import com.google.gson.Gson;

import structs.Book;
import structs.Review;

public class App {
    private static final String REVIEWS = "reviews.json";
    private static final String REVIEWS_CSV = "reviews.csv";
    private static final String BOOKS_CSV = "books.csv";
    private static final String BEST_BOOKS_CSV = "best_books.csv";

    public static void main(String[] args) throws IOException {
        Review[] reviews = readReviews();

        FileWriter file = new FileWriter(REVIEWS_CSV);
        Arrays.stream(reviews).forEach(review -> {
            try {
                file.write(review.getAuthor() + ";" + review.getText() + "\n");
            } catch (IOException e) {
                e.printStackTrace();
            }
        });
        file.close();

        FileWriter bookFile = new FileWriter(BOOKS_CSV);
        Arrays.stream(reviews).map(review -> review.getBook()).filter(distinctBook()).forEach(book -> {
            try {
                bookFile.write(book.getTitle() + ";" + book.getAuthor() + ";" + book.getCoverUrl() + "\n");
            } catch (IOException e) {
                e.printStackTrace();
            }
        });

        bookFile.close();
        
        FileWriter bestBooksFile = new FileWriter(BEST_BOOKS_CSV);
        Arrays.stream(reviews).map(review -> review.getBook()).filter(distinctBook())
                .filter(book -> book.getScore() == 5.0).forEach(book -> {
                    try {
                        bestBooksFile.write(
                                book.getTitle() + ";" + book.getAuthor() + ";" + book.getCoverUrl() + "\n");
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
        bestBooksFile.close();
    }

    private static Review[] readReviews() throws IOException {
        Gson gson = new Gson();
        return gson.fromJson(Files.readString(Paths.get(REVIEWS)), Review[].class);
    }

    public static <T> Predicate<Book> distinctBook() {
        Set<String> seen = ConcurrentHashMap.newKeySet();
        return t -> seen.add(t.getTitle() + t.getAuthor());
    }
}
