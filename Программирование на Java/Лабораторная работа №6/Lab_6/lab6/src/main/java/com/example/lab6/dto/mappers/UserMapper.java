package com.example.lab6.dto.mappers;

import org.springframework.stereotype.Service;

import com.example.lab6.dto.UserDtoRequest;
import com.example.lab6.dto.UserDtoResponse;
import com.example.lab6.models.Profile;
import com.example.lab6.models.User;

import lombok.RequiredArgsConstructor;


@RequiredArgsConstructor
@Service
public class UserMapper {
    public User toEntity(UserDtoRequest userDtoRequest) {
        User user = new User();
        user.setName(userDtoRequest.getName());
        user.setPhone(userDtoRequest.getPhone());
        user.setEmail(userDtoRequest.getEmail());
        Profile profile = new Profile();
        profile.setAddress(userDtoRequest.getAddress());
        profile.setBirthDate(userDtoRequest.getBirthDate());
        user.setProfile(profile);
        return user;
    }

    public UserDtoResponse toResponse(User user) {
        UserDtoResponse response = new UserDtoResponse(user.getId(), user.getName(), user.getPhone(), user.getEmail(),
                user.getProfile().getAddress(), user.getProfile().getBirthDate());
        return response;
    }
}
