package com.example.lab6.dto.mappers;

import org.springframework.stereotype.Service;

import com.example.lab6.dto.CategoryDtoRequest;
import com.example.lab6.dto.CategoryDtoResponse;
import com.example.lab6.models.Category;

import lombok.RequiredArgsConstructor;


@RequiredArgsConstructor
@Service
public class CategoryMapper {
    public Category toEntity(CategoryDtoRequest categoryDtoRequest) {
        Category category = new Category();
        category.setName(categoryDtoRequest.getName());
        return category;
    }

    public CategoryDtoResponse toResponse(Category category) {
        CategoryDtoResponse response = new CategoryDtoResponse(category.getId(), category.getName());
        return response;
    }
}
