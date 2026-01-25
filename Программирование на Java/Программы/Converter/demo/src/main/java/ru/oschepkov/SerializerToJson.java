package ru.oschepkov;

import java.io.File;
import java.io.IOException;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectWriter;

public class SerializerToJson {
    ObjectMapper objectMapper = new ObjectMapper();

    void apply(String path, Object obj) throws IOException {
        ObjectWriter writer = objectMapper.writerWithDefaultPrettyPrinter();
        writer.writeValue(new File(path), obj);
    }
}
