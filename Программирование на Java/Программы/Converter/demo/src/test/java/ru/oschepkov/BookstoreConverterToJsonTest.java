package ru.oschepkov;

import java.io.File;

import static org.junit.Assert.assertTrue;

import org.junit.Test;


public class BookstoreConverterToJsonTest {
    @Test
    public void testConvert() throws Exception {
        BookstoreConverterToJson conv = new BookstoreConverterToJson();
        conv.convert("src\\test\\resources\\OriginalXML.xml", "src\\test\\resources\\testConvert.json");
        assertTrue((new File("src\\test\\resources\\testConvert.json")).exists());
    }
}
