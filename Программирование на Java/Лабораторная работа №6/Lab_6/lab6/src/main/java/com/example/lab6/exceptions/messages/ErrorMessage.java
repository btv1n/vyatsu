package com.example.lab6.exceptions.messages;

import java.time.ZonedDateTime;

import lombok.Data;

@Data
public class ErrorMessage {
    private ZonedDateTime timestamp;
    private Integer status;
    private String message;
    private String path;

    public ErrorMessage(Integer status, String message, String path) {
        this.message = message;
        this.path = path;
        this.status = status;
        timestamp = ZonedDateTime.now();
    }
}
