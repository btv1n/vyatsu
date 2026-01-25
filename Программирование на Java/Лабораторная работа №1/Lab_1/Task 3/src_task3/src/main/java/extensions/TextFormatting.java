package main.java.extensions;

import java.util.Scanner;
import java.lang.String;

public class TextFormatting
{
    // Метод для преобразования текста "Как в предложениях"
    public static String toSentenceCase(String text)
    {
        StringBuilder result = new StringBuilder(text.length());
        boolean capitalize = true; // Флаг для определения, нужно ли делать букву прописной

        for (char c : text.toCharArray())
        {
            if (capitalize && Character.isLetter(c))
            {
                // Преобразуем букву в прописную и сбрасываем флаг
                result.append(Character.toUpperCase(c));
                capitalize = false;
            } else
            {
                // Остальные буквы делаем строчными
                result.append(Character.toLowerCase(c));
            }
            // Если встречаем конец предложения, включаем флаг для следующей буквы
            if (c == '.' || c == '!' || c == '?')
            {
                capitalize = true;
            }
        }
        return result.toString();
    }

    // Метод для преобразования текста в "все строчные"
    public static String toLowerCase(String text)
    {
        return text.toLowerCase(); // Все символы переводятся в нижний регистр
    }

    // Метод для преобразования текста в "ВСЕ ПРОПИСНЫЕ"
    public static String toUpperCase(String text)
    {
        return text.toUpperCase(); // Все символы переводятся в верхний регистр
    }

    // Метод для преобразования текста в "Начинать С Прописных"
    public static String toTitleCase(String text)
    {
        StringBuilder result = new StringBuilder(text.length());
        boolean capitalize = true; // Флаг для определения, нужно ли делать букву прописной

        for (char c : text.toCharArray())
        {
            if (Character.isWhitespace(c))
            {
                // При встрече пробела сбрасываем флаг для следующего слова
                capitalize = true;
                result.append(c);
            }
            else if (capitalize)
            {
                // Первая буква слова делается прописной
                result.append(Character.toUpperCase(c));
                capitalize = false;
            }
            else
            {
                // Остальные буквы делаются строчными
                result.append(Character.toLowerCase(c));
            }
        }
        return result.toString();
    }

    // Метод для преобразования текста в "иЗМЕНИТЬ РЕГИСТР"
    public static String swapCase(String text)
    {
        StringBuilder result = new StringBuilder(text.length());

        for (char c : text.toCharArray()) {
            if (Character.isUpperCase(c)) {
                // Если буква прописная, делаем строчной
                result.append(Character.toLowerCase(c));
            } else if (Character.isLowerCase(c)) {
                // Если буква строчная, делаем прописной
                result.append(Character.toUpperCase(c));
            } else {
                // Для символов без регистра оставляем без изменений
                result.append(c);
            }
        }
        return result.toString();
    }
}