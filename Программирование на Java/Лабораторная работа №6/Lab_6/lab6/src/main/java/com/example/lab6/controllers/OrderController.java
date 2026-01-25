package com.example.lab6.controllers;

import java.util.List;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.lab6.dto.OrderDtoRequest;
import com.example.lab6.dto.OrderDtoResponse;
import com.example.lab6.dto.mappers.OrderMapper;
import com.example.lab6.models.Order;
import com.example.lab6.services.OrderService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/orders")
public class OrderController {
    private final OrderService orderService;
    private final OrderMapper orderMapper;

    @GetMapping
    public ResponseEntity<List<OrderDtoResponse>> getAllOrders() {
        List<Order> orders = orderService.findAll();
        List<OrderDtoResponse> usersResponse = orders.stream().map(orderMapper::toResponse).toList();
        return ResponseEntity.status(HttpStatus.OK).body(usersResponse);
    }

    @GetMapping("/{id}")
    public ResponseEntity<OrderDtoResponse> getOrderById(@PathVariable Long id) {
        OrderDtoResponse orderResponse = orderMapper.toResponse(orderService.findById(id));
        return ResponseEntity.status(HttpStatus.OK).body(orderResponse);
    }

    @PostMapping
    public ResponseEntity<OrderDtoResponse> createOrder(@RequestBody OrderDtoRequest orderRequest) {
        Order order = orderMapper.toEntity(orderRequest);
        OrderDtoResponse orderResponse = orderMapper.toResponse(orderService.create(order));
        return ResponseEntity.status(HttpStatus.OK).body(orderResponse);
    }

    @PutMapping("/{id}")
    public ResponseEntity<OrderDtoResponse> updateOrder(@PathVariable Long id,
            @RequestBody OrderDtoRequest orderRequest) {
        Order order = orderMapper.toEntity(orderRequest);
        OrderDtoResponse orderResponse = orderMapper.toResponse(orderService.update(id, order));
        return ResponseEntity.status(HttpStatus.OK).body(orderResponse);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteOrder(@PathVariable Long id) {
        orderService.delete(id);
        return ResponseEntity.status(HttpStatus.NO_CONTENT).build();
    }
}
