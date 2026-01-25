package com.example.lab6.services;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.lab6.exceptions.ResourceNotFoundException;
import com.example.lab6.models.Order;
import com.example.lab6.repositories.OrderItemRepository;
import com.example.lab6.repositories.OrderRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class OrderService {
    private final OrderRepository orderRepository;
    private final OrderItemRepository orderItemRepository;

    public Order create(Order order) {
        Order newOrder = orderRepository.save(order);
        newOrder.getItems().forEach(item -> item.setOrder(newOrder));
        orderItemRepository.saveAll(newOrder.getItems());
        return newOrder;
    }

    public Order findById(Long id) {
        return orderRepository.findById(id).orElseThrow(
                () -> new ResourceNotFoundException("Order with id=%d does not exist".formatted(id)));
    }

    public List<Order> findAll() {
        return orderRepository.findAll();
    }

    public Order update(Long id, Order orderDetails) {
        Order order = this.findById(id);
        order.setDate(orderDetails.getDate());
        order.setStatus(orderDetails.getStatus());
        if (orderDetails.getItems() != null) {
            order.getItems().stream().forEach(item -> orderItemRepository.deleteById(item.getId()));
            orderDetails.getItems().forEach(System.out::println);
            order.setItems(orderDetails.getItems());
            order.getItems().forEach(item -> item.setOrder(order));
            orderItemRepository.saveAll(orderDetails.getItems());
        }
        return orderRepository.save(order);
    }

    public void delete(Long id) {
        orderRepository.deleteById(id);
    }
}
