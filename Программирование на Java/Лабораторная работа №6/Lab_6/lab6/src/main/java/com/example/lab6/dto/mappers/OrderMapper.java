package com.example.lab6.dto.mappers;

import java.util.List;
import java.util.Set;

import org.springframework.stereotype.Service;

import com.example.lab6.dto.OrderDtoRequest;
import com.example.lab6.dto.OrderDtoResponse;
import com.example.lab6.dto.OrderItemDtoRequest;
import com.example.lab6.dto.OrderItemDtoResponse;
import com.example.lab6.models.Order;
import com.example.lab6.models.OrderItem;
import com.example.lab6.services.UserService;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Service
public class OrderMapper {
    private final UserService userService;
    private final OrderItemMapper orderItemMapper;

    public Order toEntity(OrderDtoRequest orderDtoRequest) {
        Order order = new Order();
        order.setDate(orderDtoRequest.getDate());
        order.setStatus(orderDtoRequest.getStatus());
        order.setUser(userService.findById(orderDtoRequest.getUserId()));
        Set<OrderItem> items = order.getItems();
        for (OrderItemDtoRequest item : orderDtoRequest.getItems()) {
            items.add(orderItemMapper.toEntity(item));
        }
        return order;
    }

    public OrderDtoResponse toResponse(Order order) {
        List<OrderItemDtoResponse> items = order.getItems().stream().map(orderItemMapper::toResponse).toList();
        OrderDtoResponse response = new OrderDtoResponse(order.getId(), order.getStatus(), order.getUser().getId(), items,
                order.getDate());
        return response;
    }
}
