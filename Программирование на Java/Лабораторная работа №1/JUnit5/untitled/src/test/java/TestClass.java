import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.junit.jupiter.params.provider.CsvSource;
import org.junit.jupiter.params.provider.ValueSource;

public class TestClass {

    @Test
    void testCalcOne()
    {
        System.out.println("======TEST ONE EXECUTED=======");
        Assertions.assertEquals( 4 , 2*2);
    }

    @ParameterizedTest(name = "{index} - {0} is a palindrome")
    @ValueSource(strings = { "opo", "pop", "abba", "1" })
    void testPalindrome(String word) {
        Assertions.assertTrue(true);
    }

    @ParameterizedTest
    @CsvSource({
            "JANUARY,true",
            "FEBRUARY,false",
            "MARCH,false",
            "OCTOBER,false",
            "NOVEMBER,false"
    })
    void isOddCsvSourceTest(String monthName, boolean expectedResult) {

        Assertions.assertEquals(monthName.charAt(0)=='J', expectedResult);
    }

    @ParameterizedTest
    @CsvFileSource(resources = "file.csv", delimiter = ';')
    void isOddCsvFileSourceTest(String monthName, boolean expectedResult) {
        Assertions.assertEquals(monthName.charAt(0)=='J',expectedResult);
    }
}
