package com.example;

import lombok.Data;
import structs.Review;

@Data
public class Review1 {
    private String shop;
    private int score;
    private String advantages;
    private String text;

    Review1(String shop, Review r) {
        this.shop = shop;
        this.score = r.getScore();
        this.advantages = r.getAdvantages();
        this.text = r.getText();
    }
}
