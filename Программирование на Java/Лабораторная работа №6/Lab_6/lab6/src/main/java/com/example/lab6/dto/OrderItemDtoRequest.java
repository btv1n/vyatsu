package com.example.lab6.dto;

import lombok.Value;

@Value
public class OrderItemDtoRequest {
    private Integer quantity;
    private Integer price;
    private Long productId;
}
