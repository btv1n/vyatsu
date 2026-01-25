package com.example.lab6.services;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.lab6.exceptions.ResourceNotFoundException;
import com.example.lab6.models.User;
import com.example.lab6.repositories.UserRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class UserService {
    private final UserRepository userRepository;

    public User create(User user) {
        return userRepository.save(user);
    }

    public User findById(Long id) {
        return userRepository.findById(id).orElseThrow(
                () -> new ResourceNotFoundException("User with id=%d does not exist".formatted(id)));
    }

    public List<User> findAll() {
        return userRepository.findAll();
    }

    public User update(Long id, User userDetails) {
        User user = this.findById(id);
        user.setName(userDetails.getName());
        user.setPhone(userDetails.getPhone());
        user.setEmail(userDetails.getEmail());
        user.getProfile().setAddress(userDetails.getProfile().getAddress());
        user.getProfile().setBirthDate(userDetails.getProfile().getBirthDate());
        return userRepository.save(user);
    }

    public void delete(Long id) {
        userRepository.deleteById(id);
    }
}
