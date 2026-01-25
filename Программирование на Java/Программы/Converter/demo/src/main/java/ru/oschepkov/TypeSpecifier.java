package ru.oschepkov;

import java.io.*;

public class TypeSpecifier {
    FileType apply(String filePath) {
        try (FileReader reader = new FileReader(filePath)) {
            int character = reader.read();
            if (character != -1) {
                char firstChar = (char) character;
                switch (firstChar) {
                    case '<': return FileType.XML;
                    case '{': return FileType.JSON;
                    default: return FileType.UNKNOWN;
                }
            } else if (filePath.endsWith(".xml")) {
                return FileType.XML;
            } else if (filePath.endsWith(".json")) {
                return FileType.JSON;
            } else {
                return FileType.UNKNOWN;
            }
        } catch (IOException e) {
            System.out.println("Ошибка чтения файла: " + e.getMessage());
            return FileType.UNKNOWN;
        }
    }
}
