package ru.oschepkov;

import java.io.File;
import java.io.IOException;

import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;

public class SerializerToXml {
    XmlMapper xmlMapper = new XmlMapper();

    void apply(String path, Object obj) throws IOException {
        xmlMapper.enable(SerializationFeature.INDENT_OUTPUT);
        xmlMapper.writeValue(new File(path), obj);
    }
}
