package com.example.lab6.exceptions.handlers;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

import com.example.lab6.exceptions.ResourceNotFoundException;
import com.example.lab6.exceptions.messages.ErrorMessage;

import jakarta.servlet.http.HttpServletRequest;

@ControllerAdvice
public class CustomExceptionHandler {
    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<?> handleResourceNotFoundException(ResourceNotFoundException e, HttpServletRequest request) {
        ErrorMessage errorMessage = new ErrorMessage(HttpStatus.NOT_FOUND.value(), e.getMessage(), request.getRequestURI());
        return buildResponseEntity(errorMessage);
    }

    private static ResponseEntity<?> buildResponseEntity(ErrorMessage errorMessage) {
        return ResponseEntity.status(errorMessage.getStatus()).body(errorMessage);
    }
}
