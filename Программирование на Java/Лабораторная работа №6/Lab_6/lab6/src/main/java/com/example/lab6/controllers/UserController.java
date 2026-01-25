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

import com.example.lab6.dto.UserDtoRequest;
import com.example.lab6.dto.UserDtoResponse;
import com.example.lab6.dto.mappers.UserMapper;
import com.example.lab6.models.User;
import com.example.lab6.services.UserService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/users")
public class UserController {
    private final UserService userService;
    private final UserMapper userMapper;

    @GetMapping
    public ResponseEntity<List<UserDtoResponse>> getAllUsers() {
        List<User> users = userService.findAll();
        List<UserDtoResponse> usersResponse = users.stream().map(userMapper::toResponse).toList();
        return ResponseEntity.status(HttpStatus.OK).body(usersResponse);
    }

    @GetMapping("/{id}")
    public ResponseEntity<UserDtoResponse> getUserById(@PathVariable Long id) {
        UserDtoResponse userResponse = userMapper.toResponse(userService.findById(id));
        return ResponseEntity.status(HttpStatus.OK).body(userResponse);
    }

    @PostMapping
    public ResponseEntity<UserDtoResponse> createUser(@RequestBody UserDtoRequest userRequest) {
        User user = userMapper.toEntity(userRequest);
        UserDtoResponse userResponse = userMapper.toResponse(userService.create(user));
        return ResponseEntity.status(HttpStatus.OK).body(userResponse);
    }

    @PutMapping("/{id}")
    public ResponseEntity<UserDtoResponse> updateUser(@PathVariable Long id, @RequestBody UserDtoRequest userRequest) {
        User user = userMapper.toEntity(userRequest);
        UserDtoResponse userResponse = userMapper.toResponse(userService.update(id, user));
        return ResponseEntity.status(HttpStatus.OK).body(userResponse);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteUser(@PathVariable Long id) {
        userService.delete(id);
        return ResponseEntity.status(HttpStatus.NO_CONTENT).build();
    }
}
