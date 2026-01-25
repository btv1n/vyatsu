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

import com.example.lab6.dto.ProductDtoRequest;
import com.example.lab6.dto.ProductDtoResponse;
import com.example.lab6.dto.mappers.ProductMapper;
import com.example.lab6.models.Product;
import com.example.lab6.services.ProductService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/products")
public class ProductController {
    private final ProductService productService;
    private final ProductMapper productMapper;

    @GetMapping
    public ResponseEntity<List<ProductDtoResponse>> getAllProducts() {
        List<Product> orders = productService.findAll();
        List<ProductDtoResponse> productsResponse = orders.stream().map(productMapper::toResponse).toList();
        return ResponseEntity.status(HttpStatus.OK).body(productsResponse);
    }

    @GetMapping("/{id}")
    public ResponseEntity<ProductDtoResponse> getProductById(@PathVariable Long id) {
        ProductDtoResponse productResponse = productMapper.toResponse(productService.findById(id));
        return ResponseEntity.status(HttpStatus.OK).body(productResponse);
    }

    @PostMapping
    public ResponseEntity<ProductDtoResponse> createProduct(@RequestBody ProductDtoRequest productRequest) {
        Product product = productMapper.toEntity(productRequest);
        ProductDtoResponse productResponse = productMapper.toResponse(productService.create(product));
        return ResponseEntity.status(HttpStatus.OK).body(productResponse);
    }

    @PutMapping("/{id}")
    public ResponseEntity<ProductDtoResponse> updateProduct(@PathVariable Long id,
            @RequestBody ProductDtoRequest productRequest) {
        Product product = productMapper.toEntity(productRequest);
        ProductDtoResponse productResponse = productMapper.toResponse(productService.update(id, product));
        return ResponseEntity.status(HttpStatus.OK).body(productResponse);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteProduct(@PathVariable Long id) {
        productService.delete(id);
        return ResponseEntity.status(HttpStatus.NO_CONTENT).build();
    }
}
