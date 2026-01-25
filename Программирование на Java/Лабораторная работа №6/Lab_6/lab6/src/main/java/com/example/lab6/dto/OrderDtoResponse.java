package com.example.lab6.dto;

import java.util.Date;
import java.util.List;

import org.springframework.format.annotation.DateTimeFormat;

import com.example.lab6.enums.OrderStatus;

import lombok.Value;

@Value
public class OrderDtoResponse {
    Long id;
    OrderStatus status;
    Long userId;
    List<OrderItemDtoResponse> items;
    @DateTimeFormat(iso = DateTimeFormat.ISO.DATE)
    Date date;
}
