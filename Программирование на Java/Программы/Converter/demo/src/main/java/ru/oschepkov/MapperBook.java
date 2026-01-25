package ru.oschepkov;

import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper
public interface MapperBook {
    
    @Mapping(target = ".", source = ".")
    ru.oschepkov.BookstoreStruct.Book convert(ru.oschepkov.BookstoreNewStruct.Book bookstore);

    @Mapping(target = ".", source = ".")
    ru.oschepkov.BookstoreNewStruct.Book convert(ru.oschepkov.BookstoreStruct.Book bookstore);
}
