package test;
import main.java.extensions.StringExtention;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import static org.junit.jupiter.api.Assertions.assertEquals;


//public void testTruncateWithNegativeNumber()

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


public class StringExtentionTest
{
    //Transliteration
    @Test
    public void testTransliterationWithCustomDivider()
    {
        assertEquals("Ivanov_John", StringExtention.transliteration("Иванов John", "_"));
    }

    @Test
    public void testTransliterationWithCustomDivider2()
    {
        assertEquals("Privet*Mir", StringExtention.transliteration("Привет Мир", "*"));
    }

    @Test
    public void testTransliterationWithCustomDivider3()
    {
        assertEquals("RaDuGaЁЁЁDuGa", StringExtention.transliteration("РаДуГа ДуГа", "ЁЁЁ"));
    }

    // Разделитель поумолчанию пробел - " ", его можно не указывать в тесте явно.
    @Test
    public void testTransliterationWithDefaultDivider()
    {
        assertEquals("Ivan Ivanov", StringExtention.transliteration("Иван Иванов"));
    }

    @Test
    public void testTransliterationWithDefaultDivider2()
    {
        assertEquals("Shalaginova Nadezhda Vladimirovna", StringExtention.transliteration("Шалагинова Надежда Владимировна", " "));
    }

    @Test
    public void testTransliterationWithDefaultDivider3()
    {
        assertEquals("Murtuzaev Telman Islam ogli", StringExtention.transliteration("Муртузаев Тельман Ислам оглы", " "));
    }
    //End Transliteration

    //Truncate
    @Test
    public void testTruncateDefaultLength()
    {
        assertEquals("39 новых фич, ко...", StringExtention.truncate("39 новых фич, которые будут доступны в Java 12"));
    }

    @Test
    public void testTruncateDefaultLength1()
    {
        assertEquals("398979 897  9898...", StringExtention.truncate("398979 897  9898 8"));
    }

    @Test
    public void testTruncateCustomLength()
    {
        assertEquals("39 новых...", StringExtention.truncate("39 новых фич, которые будут доступны в Java 12", 9));
    }

    @Test
    public void testTruncateCustomLength2()
    {
        assertEquals("ОкТяБ...", StringExtention.truncate("ОкТяБрЬ", 5));
    }

    @Test
    public void testTruncateCustomLength3()
    {
        assertEquals("    А", StringExtention.truncate("    А", 5));
    }

    @Test
    public void testTruncateCustomLength4()
    {
        assertEquals("...", StringExtention.truncate("    А", 4));
    }

    @Test ///
    public void testTruncateWithTrailingSpaces()
    {
        assertEquals("A", StringExtention.truncate("A                ", 3));
    }

    @Test
    public void testTruncateWithTrailingSpaces2()
    {
        assertEquals("В...", StringExtention.truncate("В           В В", 3));
    }
    //End Truncate


    /////////////////////////////////////////////////////////////////////////////////////
    // Тексты с параметрами
    @ParameterizedTest // указывает, что текст нужно запускать несколько раз
    @CsvSource({ // список параметров
            "'Иванов John', 'Ivanov_John', '_'",
            "'РаДуГа ДуГа', 'RaDuGaЁЁЁDuGa', 'ЁЁЁ'",
            "'Шалагинова Надежда Владимировна', 'Shalaginova Nadezhda Vladimirovna', ' '"
    })
    void testTransliterationWithDivider(String input, String expected, String divider) {
        String actual = StringExtention.transliteration(input, divider);
        assertEquals(expected, actual); // проверка результата
    }

    @ParameterizedTest
    @CsvSource({
            "'Иван Иванов', 'Ivan Ivanov'",
            "'Шалагинова Надежда Владимировна', 'Shalaginova Nadezhda Vladimirovna'",
            "'Жанна Шишкина', 'Zhanna Shishkina'"
    })
    void testTransliterationWithDefaultDivider(String input, String expected) {
        String actual = StringExtention.transliteration(input);
        assertEquals(expected, actual);
    }

    @ParameterizedTest
    @CsvSource({
            "'39 новых фич, которые будут доступны в Java 12', '39 новых фич, ко...'",
            "'398979 897  9898 8', '398979 897  9898...', "
    })
    void testTruncateCustomLengthDefaultDivider(String input, String expected) {
        String actual = StringExtention.truncate(input);
        assertEquals(expected, actual);
    }

    @ParameterizedTest
    @CsvSource({
            "'39 новых фич, которые будут доступны в Java 12', 9, '39 новых...'",
            "'ОкТяБрЬ', 5, 'ОкТяБ...'",
            "'    А', 5, '    А'",
            "'    А', 4, '...'",
            "'A                ', 3, 'A'",
            "'В           В В', 3, 'В...'",
            "'   ', 1, ''",
            "'   ', 0, ''",
            "'    А', 0, ''",
            "'    А', -4, ''"
    })
    void testTruncateCustomLengthDefaultDivider(String input, int length, String expected) {
        String actual = StringExtention.truncate(input, length);
        assertEquals(expected, actual);
    }
    //  если 0, то пустая строка
    //  если отрицательное число, то пустая строка или ads(number)
}