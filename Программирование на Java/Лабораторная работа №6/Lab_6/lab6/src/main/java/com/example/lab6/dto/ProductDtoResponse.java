package com.example.lab6.dto;

import java.util.List;

import lombok.Value;

@Value
public class ProductDtoResponse {
    private Long id;
    private String name;
    private String description;
    private Integer price;
    private List<Long> categories;
}
