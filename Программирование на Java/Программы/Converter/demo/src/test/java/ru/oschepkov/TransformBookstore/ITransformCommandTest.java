package ru.oschepkov.TransformBookstore;

import static org.junit.Assert.assertTrue;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

import org.junit.Test;

import ru.oschepkov.BookstoreStruct.Book;
import ru.oschepkov.BookstoreStruct.Bookstore;
import ru.oschepkov.BookstoreStruct.Price;
import ru.oschepkov.BookstoreStruct.Title;
import ru.oschepkov.BookstoreStruct.Character;

public class ITransformCommandTest {
    @Test
    public void testFunctionInterface() {
         List<Character> characters1 = new ArrayList<>();
            characters1.add(new Character("Harry Potter", "Protagonist"));
            characters1.add(new Character("Hermione Granger", "Supporting"));
            characters1.add(new Character("Ron Weasley", "Supporting"));

            List<Character> characters2 = new ArrayList<>();
            characters2.add(new Character("Paul Atreides", "Protagonist"));
            characters2.add(new Character("Liet-Kynes", "Supporting"));
            characters2.add(new Character("Baron Vladimir Harkonnen", "Antagonist"));

            Bookstore bookstore = new Bookstore();
            bookstore.setBooks(new ArrayList<>());

            Book book1 = new Book();
            book1.setCategory("Fantasy");
            book1.setTitle(new Title("en", "Harry Potter and the Philosopher's Stone"));
            book1.setAuthor("J.K. Rowling");
            book1.setYear(1997);
            book1.setPrice(new Price("USD", 25));
            book1.setCharacters(characters1);

            Book book2 = new Book();
            book2.setCategory("Science Fiction");
            book2.setTitle(new Title("en", "Dune"));
            book2.setAuthor("Frank Herbert");
            book2.setYear(1965);
            book2.setPrice(new Price("USD", 20));
            book2.setCharacters(characters2);

            bookstore.getBooks().add(book1);
            bookstore.getBooks().add(book2);

            ITransformCommand command = (Bookstore b) -> {
                return new Bookstore(b.getBooks().stream()
                    .sorted(Comparator.comparing(book -> book.getPrice().getValue()))
                    .collect(Collectors.toList()));
            };
            bookstore = command.apply(bookstore);

            assertTrue(Math.abs(bookstore.getBooks().get(0).getPrice().getValue() - 20) < Math.pow(-10, 6));
    }
}
