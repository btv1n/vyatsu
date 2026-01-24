package org.example;

import lombok.Builder;
import lombok.NonNull;

@Builder
public class Person {
    @NonNull
    String name;
    final int age;
}
