package com.example;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Arrays;

import com.google.gson.Gson;

import lombok.var;
import structs.OnlineShop;

public class App {
    private static final String FILE = "shops.json";

    public static void main(String[] args) throws IOException {
        OnlineShop[] shops = Arrays.stream(readShops()).filter(shop -> shop.getReviews().length > 0)
                .toArray(OnlineShop[]::new);
        Arrays.stream(shops)
                .flatMap(shop -> Arrays.stream(shop.getReviews())
                        .filter(review -> review.getDisadvantages().equalsIgnoreCase("не обноружил")
                                || review.getDisadvantages().equalsIgnoreCase("Нет")
                                || review.getDisadvantages().equals("-") || review.getDisadvantages().equals("—"))
                        .map(review -> new Review1(shop.getName(), review)))
                .forEach(System.out::println);

        Arrays.stream(shops).forEach(shop -> System.out.println(shop.getName() + ": "
                + Arrays.stream(shop.getReviews()).filter(review -> review.getScore() <= 2).count()));

        Arrays.stream(shops).forEach(shop -> {
            var negative = Arrays.stream(shop.getReviews()).filter(review -> review.getScore() < 3).toList();
            var neutral = Arrays.stream(shop.getReviews()).filter(review -> review.getScore() == 3).toList();
            var positive = Arrays.stream(shop.getReviews()).filter(review -> review.getScore() > 3).toList();
            System.out.println(shop.getName() + ": (+) " + positive.size() + ", (0) " + neutral.size() + ", (-) "
                    + negative.size());
        });
    }

    private static OnlineShop[] readShops() throws IOException {
        Gson gson = new Gson();
        String json = Files.readString(Paths.get(FILE));
        return gson.fromJson(json, OnlineShop[].class);
    }
}
