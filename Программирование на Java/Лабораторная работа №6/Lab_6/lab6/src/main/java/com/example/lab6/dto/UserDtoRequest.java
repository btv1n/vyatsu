package com.example.lab6.dto;

import java.util.Date;

import org.springframework.format.annotation.DateTimeFormat;

import lombok.Value;

@Value
public class UserDtoRequest {
    String name;
    String phone;
    String email;
    String address;
    
    @DateTimeFormat(iso = DateTimeFormat.ISO.DATE)
    Date birthDate;
}
