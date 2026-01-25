package com.example.lab6.services;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.springframework.stereotype.Service;

import com.example.lab6.exceptions.ResourceNotFoundException;
import com.example.lab6.models.Category;
import com.example.lab6.repositories.CategoryRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class CategoryService {
    private final CategoryRepository categoryRepository;

    public Category create(Category category) {
        return categoryRepository.save(category);
    }

    public Category findById(Long id) {
        return categoryRepository.findById(id).orElseThrow(
                () -> new ResourceNotFoundException("Category with id=%d does not exist".formatted(id)));
    }

    public Set<Category> findAllById(Set<Long> idx) {
        Set<Category> result = new HashSet<>();
        List<Category> categories = categoryRepository.findAllById(idx);
        if (categories.size() != idx.size()) {
            throw new ResourceNotFoundException("Some categories does not exist");
        }
        result.addAll(categories);
        return result;
    }

    public List<Category> findAll() {
        return categoryRepository.findAll();
    }

    public Category update(Long id, Category categoryDetails) {
        Category category = this.findById(id);
        category.setName(categoryDetails.getName());
        return categoryRepository.save(category);
    }

    public void delete(Long id) {
        categoryRepository.deleteById(id);
    }
}
