package ru.oschepkov.BookstoreNewStruct;

import java.util.List;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class Book {

    @JacksonXmlProperty(isAttribute = true)
    private String category;
    private Title title;
    private String author;
    private int year;
    private Price price;
    
    @JacksonXmlProperty(localName = "character")
    @JacksonXmlElementWrapper(localName = "characters")
    private List<Character> characters;
}
