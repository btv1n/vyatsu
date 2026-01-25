package ru.oschepkov.TransformBookstore;

import java.util.Comparator;
import java.util.stream.Collectors;

import ru.oschepkov.BookstoreStruct.Book;
import ru.oschepkov.BookstoreStruct.Bookstore;

public class SortByYears implements ITransformCommand{

    @Override
    public Bookstore apply(final Bookstore bookstore) {
       return new Bookstore(bookstore.getBooks().stream()
            .sorted(Comparator.comparing(Book::getYear))
            .collect(Collectors.toList()));
    }
    
}
