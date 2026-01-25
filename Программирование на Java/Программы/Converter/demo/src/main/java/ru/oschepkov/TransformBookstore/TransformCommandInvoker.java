package ru.oschepkov.TransformBookstore;

import lombok.AllArgsConstructor;
import ru.oschepkov.BookstoreStruct.Bookstore;

@AllArgsConstructor
public class TransformCommandInvoker {
    ITransformCommand command;

    void setCommand(ITransformCommand command) {
        this.command = command;
    }

    Bookstore invoke(Bookstore bookstore) {
        return command.apply(bookstore);
    }
}
