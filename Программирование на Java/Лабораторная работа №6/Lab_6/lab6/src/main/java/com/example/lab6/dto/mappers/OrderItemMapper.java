package com.example.lab6.dto.mappers;

import org.springframework.stereotype.Service;

import com.example.lab6.dto.OrderItemDtoRequest;
import com.example.lab6.dto.OrderItemDtoResponse;
import com.example.lab6.models.OrderItem;
import com.example.lab6.services.ProductService;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Service
public class OrderItemMapper {
    private final ProductService productService;

    public OrderItem toEntity(OrderItemDtoRequest orderItemDtoRequest) {
        OrderItem item = new OrderItem();
        item.setQuantity(orderItemDtoRequest.getQuantity());
        item.setPrice(orderItemDtoRequest.getPrice());
        item.setProduct(productService.findById(orderItemDtoRequest.getProductId()));
        return item;
    }

    public OrderItemDtoResponse toResponse(OrderItem orderItem) {
        OrderItemDtoResponse response = new OrderItemDtoResponse(orderItem.getId(), orderItem.getQuantity(), orderItem.getPrice(),
                orderItem.getProduct().getId());
        return response;
    }
}
