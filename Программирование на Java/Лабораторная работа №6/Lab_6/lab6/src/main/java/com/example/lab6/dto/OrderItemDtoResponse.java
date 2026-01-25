package com.example.lab6.dto;

import lombok.Value;

@Value
public class OrderItemDtoResponse {
    private Long id;
    private Integer quantity;
    private Integer price;
    private Long productId;
}
