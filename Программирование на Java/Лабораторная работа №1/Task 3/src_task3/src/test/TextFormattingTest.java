package test;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;
import main.java.extensions.TextFormatting;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.MethodSource;


import org.junit.jupiter.params.provider.CsvSource;

/*
assertEquals(expected, actual) - Этот метод используется для проверки, что ожидаемое значение равно фактическому.
expected — ожидаемое значение.
actual — фактическое значение, которое возвращает тестируемый код.

assertTrue(condition) - Этот метод проверяет, что условие (boolean выражение) истинно.

assertFalse(condition) - Этот метод проверяет, что условие (boolean выражение) ложно.

assertNull(object) - Этот метод проверяет, что объект равен null.

assertNotNull(object) - Этот метод проверяет, что объект не равен null.

assertArrayEquals(expected, actual) - Этот метод проверяет, что два массива равны (сравниваются по содержимому).
expected — ожидаемый массив.
actual — фактический массив.

assertThrows(expectedType, executable) - Этот метод проверяет, что при выполнении кода (executable) выбрасывается ожидаемое исключение.
expectedType — ожидаемый тип исключения.
executable — код, который должен выбросить исключение.

assertDoesNotThrow(executable) - Этот метод проверяет, что при выполнении кода не выбрасывается исключение.
executable — код, который не должен выбросить исключение.
*/

public class TextFormattingTest
{
    @Test
    public void toSentenceCaseTest1()
    {
        assertEquals("Иван john", TextFormatting.toSentenceCase("Иван John"));
    }

    @Test
    public void toLowerCaseTest1()
    {
        assertEquals("иван john", TextFormatting.toLowerCase("Иван John"));
    }

    @Test
    public void toUpperCaseTest1()
    {
        assertEquals("ИВАН JOHN", TextFormatting.toUpperCase("Иван John"));
    }

    @Test
    public void toTitleCaseTest1()
    {
        assertEquals("Иван John", TextFormatting.toTitleCase("иван john"));
    }

    @Test
    public void swapTextCaseTest1()
    {
        assertEquals("иВАН jOHN", TextFormatting.swapCase("Иван John"));
    }


    // Тесты с параметрами
//    @ParameterizedTest
//    @MethodSource("sentenceCaseProvider")
//    void toSentenceCaseTest(String input, String expected) {
//        assertEquals(expected, TextFormatting.toSentenceCase(input));
//    }
//    static Stream<Arguments> sentenceCaseProvider() {
//        return Stream.of(
//                Arguments.of("Иван John", "Иван john"),
//                Arguments.of("Привет МИР", "Привет мир"),
//                Arguments.of("hello WORLD", "Hello world")
//        );
//    }




    // Тесты с параметрами
    @ParameterizedTest
    @CsvSource({
            "Иван John, Иван john",
            "Привет МИР, Привет мир",
            "hello WORLD, Hello world",
            "пример текста. пример текста, Пример текста. Пример текста"
    })
    void toSentenceCaseTest2(String input, String expected) {
        assertEquals(expected, TextFormatting.toSentenceCase(input));
    }

    @ParameterizedTest
    @CsvSource({
            "Иван John, иван john",
            "Привет МИР, привет мир",
            "hello WORLD, hello world"
    })
    void toLowerCaseTest2(String input, String expected) {
        assertEquals(expected, TextFormatting.toLowerCase(input));
    }

    @ParameterizedTest
    @CsvSource({
            "Иван John, ИВАН JOHN",
            "Привет мир, ПРИВЕТ МИР",
            "Hello world, HELLO WORLD"
    })
    void toUpperCaseTest2(String input, String expected) {
        assertEquals(expected, TextFormatting.toUpperCase(input));
    }

    @ParameterizedTest
    @CsvSource({
            "иван john, Иван John",
            "привет мир, Привет Мир",
            "hello world, Hello World"
    })
    void toTitleCaseTest2(String input, String expected) {
        assertEquals(expected, TextFormatting.toTitleCase(input));
    }

    @ParameterizedTest
    @CsvSource({
            "Иван John, иВАН jOHN",
            "Привет МИР, пРИВЕТ мир",
            "Hello World, hELLO wORLD"
    })
    void swapTextCaseTest2(String input, String expected) {
        assertEquals(expected, TextFormatting.swapCase(input));
    }
}