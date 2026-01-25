package com.example.lab6.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;

import com.example.lab6.dto.UserDtoRequest;
import com.example.lab6.dto.mappers.UserMapper;
import com.example.lab6.services.UserService;

import lombok.RequiredArgsConstructor;

@Controller
@RequiredArgsConstructor
@RequestMapping("/users")
public class UserViewController {
    private final UserService userService;
    private final UserMapper userMapper;

    @GetMapping
    public String getAllUsers(Model model) {
        model.addAttribute("users", userService.findAll());
        return "user/users";
    }

    @GetMapping("/create")
    public String showCreateForm(Model model) {
        return "user/create-user";
    }

    @PostMapping
    public String createStudent(@ModelAttribute UserDtoRequest user) {
        userService.create(userMapper.toEntity(user));
        return "redirect:/users";
    }

    @GetMapping("/{id}")
    public String getUserById(@PathVariable Long id, Model model) {
        model.addAttribute("user", userService.findById(id));
        return "user/user";
    }

    @GetMapping("/{id}/edit")
    public String showEditForm(@PathVariable Long id, Model model) {
        model.addAttribute("user", userService.findById(id));
        return "user/edit-user";
    }

    @PostMapping("/{id}")
    public String editUser(@PathVariable Long id, @ModelAttribute UserDtoRequest user) {
        userService.update(id, userMapper.toEntity(user));
        return "redirect:/users";
    }

    @DeleteMapping("/{id}")
    public String deleteStudent(@PathVariable Long id) {
        userService.delete(id);
        return "redirect:/users";
    }
}
