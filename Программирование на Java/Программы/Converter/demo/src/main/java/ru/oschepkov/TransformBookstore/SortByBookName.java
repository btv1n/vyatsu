package ru.oschepkov.TransformBookstore;

import java.util.Comparator;
import java.util.stream.Collectors;

import ru.oschepkov.BookstoreStruct.Bookstore;

public class SortByBookName implements ITransformCommand {
    
    @Override
    public Bookstore apply(final Bookstore bookstore) {
        return new Bookstore(bookstore.getBooks().stream()
            .sorted(Comparator.comparing(book -> book.getTitle().getValue()))
            .collect(Collectors.toList()));
    }
}
