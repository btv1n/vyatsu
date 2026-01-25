package ru.oschepkov;

import java.io.File;
import java.io.IOException;

import com.fasterxml.jackson.databind.ObjectMapper;

import ru.oschepkov.BookstoreNewStruct.Years;

public class DeserializerFromJson {
    ObjectMapper jsonMapper = new ObjectMapper();
    Years apply(String path) throws IOException{
        File file = new File(path);
        return jsonMapper.readValue(file, Years.class);
    }
}
