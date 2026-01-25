package ru.oschepkov;

import static org.junit.Assert.assertTrue;

import java.io.File;

import org.junit.Test;

public class BookstoreConverterToXmlTest {
    @Test
    public void testConvert() throws Exception { 

        BookstoreConverterToXml conv = new BookstoreConverterToXml();
        conv.convert("src\\test\\resources\\testConvert.xml", "src\\test\\resources\\OriginalJSON.json");
        assertTrue((new File("src\\test\\resources\\testConvert.xml")).exists());
    }
}
