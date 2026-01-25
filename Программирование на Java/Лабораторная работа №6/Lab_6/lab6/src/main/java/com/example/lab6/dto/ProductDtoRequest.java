package com.example.lab6.dto;

import java.util.Set;

import lombok.Value;

@Value
public class ProductDtoRequest {
    private String name;
    private String description;
    private Integer price;
    private Set<Long> categories;
}
