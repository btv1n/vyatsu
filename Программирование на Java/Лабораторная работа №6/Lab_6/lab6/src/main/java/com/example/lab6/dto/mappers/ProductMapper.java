package com.example.lab6.dto.mappers;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.lab6.dto.ProductDtoRequest;
import com.example.lab6.dto.ProductDtoResponse;
import com.example.lab6.models.Product;
import com.example.lab6.services.CategoryService;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Service
public class ProductMapper {
    private final CategoryService categoryService;

    public Product toEntity(ProductDtoRequest productDtoRequest) {
        Product product = new Product();
        product.setName(productDtoRequest.getName());
        product.setDescription(productDtoRequest.getDescription());
        product.setPrice(productDtoRequest.getPrice());
        product.setCategories(categoryService.findAllById(productDtoRequest.getCategories()));
        return product;
    }

    public ProductDtoResponse toResponse(Product product) {
        List<Long> categories = product.getCategories().stream().map(category -> category.getId()).toList();
        ProductDtoResponse response = new ProductDtoResponse(product.getId(), product.getName(),
                product.getDescription(),
                product.getPrice(),
                categories);
        return response;
    }
}
