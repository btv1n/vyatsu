package ru.oschepkov;

import java.io.IOException;



public class BookstoreConverterToXml implements IConverter{

    final SerializerToXml serializerToXml;
    final DeserializerFromJson deserializerFromJson;
    final MapperBookstore mapperBookStore;

    BookstoreConverterToXml() throws Exception {
        serializerToXml = new SerializerToXml();
        deserializerFromJson = new DeserializerFromJson();
        mapperBookStore = new MapperBookstore();
    }

    @Override
    public void convert(String pathXml, String pathJson){

        try {
            ru.oschepkov.BookstoreNewStruct.Years years = deserializerFromJson.apply(pathJson);
            serializerToXml.apply(pathXml, mapperBookStore.convert(years));
        } catch(IOException e) {
            e.printStackTrace();
        }
        
    }

    @Override
    public void convert(String pathXml){
        convert(pathXml, "src\\main\\resourses\\out.json");
    }  
}
