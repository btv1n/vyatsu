package ru.oschepkov.BookstoreNewStruct;

import java.util.List;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlText;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class Year {

    @JacksonXmlText
    private int value;

    @JacksonXmlProperty(localName = "book")
    private List<Book> books;
}

