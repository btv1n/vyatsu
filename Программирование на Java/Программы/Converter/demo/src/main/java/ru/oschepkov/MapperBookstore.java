package ru.oschepkov;

import ru.oschepkov.BookstoreNewStruct.Year;
import ru.oschepkov.BookstoreNewStruct.Years;
import ru.oschepkov.BookstoreStruct.Book;
import ru.oschepkov.BookstoreStruct.Bookstore;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class MapperBookstore {

    Years convert(Bookstore bookstore) {
        MapperBook mapperBook = new MapperBookImpl();
        List<Year> years = new ArrayList();
        Map<Integer, List<Book>> map = bookstore.getBooks().stream()
            .collect(Collectors.groupingBy(Book::getYear));
        for (var entry : map.entrySet()) {
            Year year = Year.builder()
                .books(entry.getValue().stream()
                    .map(mapperBook::convert)
                    .collect(Collectors.toList()))
                .value(entry.getKey())
                .build();
            years.add(year);
        }
        return new Years(years);
    } 

    Bookstore convert(Years years) {
        MapperBook mapperBook = new MapperBookImpl();
        List<Book> books = new ArrayList();
        years.getYears().forEach(year -> {
            books.addAll(year.getBooks().stream()
                .map(mapperBook::convert)
                .collect(Collectors.toList()));
        });
        return new Bookstore(books);
    }
}
