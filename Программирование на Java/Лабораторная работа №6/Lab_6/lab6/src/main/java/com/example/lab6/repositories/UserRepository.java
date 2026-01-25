package com.example.lab6.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.example.lab6.models.User;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
}
