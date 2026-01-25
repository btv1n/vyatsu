package com.example.lab6.services;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.lab6.exceptions.ResourceNotFoundException;
import com.example.lab6.models.Product;
import com.example.lab6.repositories.ProductRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class ProductService {
    private final ProductRepository productRepository;

    public Product create(Product product) {
        return productRepository.save(product);
    }

    public Product findById(Long id) {
        return productRepository.findById(id).orElseThrow(
                () -> new ResourceNotFoundException("Product with id=%d does not exist".formatted(id)));
    }

    public List<Product> findAll() {
        return productRepository.findAll();
    }

    public Product update(Long id, Product productDetails) {
        Product product = this.findById(id);
        product.setDescription(productDetails.getDescription());
        product.setName(productDetails.getName());
        product.setCategories(productDetails.getCategories());
        product.setPrice(productDetails.getPrice());
        return productRepository.save(product);
    }

    public void delete(Long id) {
        productRepository.deleteById(id);
    }
}
