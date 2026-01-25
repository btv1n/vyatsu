package ru.oschepkov;

import java.io.IOException;


public class BookstoreConverterToJson implements IConverter {

    final SerializerToJson serializerToJson;
    final DeserializerFromXml deserializerFromXml;
    final MapperBookstore mapperBookStore;

    BookstoreConverterToJson() throws Exception {
        serializerToJson = new SerializerToJson();
        deserializerFromXml = new DeserializerFromXml();
        mapperBookStore = new MapperBookstore();
    }

    @Override
    public void convert(String pathXml, String pathJson) {
        try {
            ru.oschepkov.BookstoreStruct.Bookstore bookstore = deserializerFromXml.apply(pathXml);
            serializerToJson.apply(pathJson, mapperBookStore.convert(bookstore));
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    @Override
    public void convert(String pathXml){
        convert(pathXml, "src\\test\\resources\\BookstoreConverterToJson.json");
    }
}
