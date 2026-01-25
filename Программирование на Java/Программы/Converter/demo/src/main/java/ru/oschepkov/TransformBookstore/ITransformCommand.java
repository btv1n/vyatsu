package ru.oschepkov.TransformBookstore;

import ru.oschepkov.BookstoreStruct.Bookstore;

@FunctionalInterface
public interface ITransformCommand {
    Bookstore apply(Bookstore bookstore);
}
