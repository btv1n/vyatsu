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

import com.example.lab6.dto.CategoryDtoRequest;
import com.example.lab6.dto.CategoryDtoResponse;
import com.example.lab6.dto.mappers.CategoryMapper;
import com.example.lab6.models.Category;
import com.example.lab6.services.CategoryService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/categories")
public class CategoryController {
    private final CategoryService categoryService;
    private final CategoryMapper categoryMapper;

    @GetMapping
    public ResponseEntity<List<CategoryDtoResponse>> getAllCategories() {
        List<Category> categories = categoryService.findAll();
        List<CategoryDtoResponse> usersResponse = categories.stream().map(categoryMapper::toResponse).toList();
        return ResponseEntity.status(HttpStatus.OK).body(usersResponse);
    }

    @GetMapping("/{id}")
    public ResponseEntity<CategoryDtoResponse> getOrderById(@PathVariable Long id) {
        CategoryDtoResponse categoryResponse = categoryMapper.toResponse(categoryService.findById(id));
        return ResponseEntity.status(HttpStatus.OK).body(categoryResponse);
    }

    @PostMapping
    public ResponseEntity<CategoryDtoResponse> createOrder(@RequestBody CategoryDtoRequest categoryRequest) {
        Category category = categoryMapper.toEntity(categoryRequest);
        CategoryDtoResponse categoryResponse = categoryMapper.toResponse(categoryService.create(category));
        return ResponseEntity.status(HttpStatus.OK).body(categoryResponse);
    }

    @PutMapping("/{id}")
    public ResponseEntity<CategoryDtoResponse> updateOrder(@PathVariable Long id,
            @RequestBody CategoryDtoRequest categoryRequest) {
        Category category = categoryMapper.toEntity(categoryRequest);
        CategoryDtoResponse categoryResponse = categoryMapper.toResponse(categoryService.update(id, category));
        return ResponseEntity.status(HttpStatus.OK).body(categoryResponse);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteOrder(@PathVariable Long id) {
        categoryService.delete(id);
        return ResponseEntity.status(HttpStatus.NO_CONTENT).build();
    }
}
