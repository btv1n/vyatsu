package main.java;
import java.util.Scanner;
import java.lang.String;
import main.java.extensions.StringExtention;

public class Main
{
    public static void main(String[] args)
    {
        // Создаем объект Scanner для считывания данных с клавиатуры
        Scanner scanner = new Scanner(System.in);

        // Использование метода transliteration
        System.out.println("Введите текст для транслитерации на кириллице: ");
        String inputText;
        while (true)
        {
            inputText = scanner.nextLine(); // считывание данных пользователя

            // Проверка, содержит ли стока цифры или символы
            if (inputText.matches("^[^0-9a-zA-Z!\"№;%:?*()_+@#$%^&*`\\[\\]{}<>.,|\\\\/~]*$"))
            {
                break;
            }
            else
            {
                System.out.println("Ошибка: строка не должна содержать цифры, символы и латинский алфавит");
            }
        }

        System.out.println("Введите разделитель для транслитерации (по умолчанию - пробел): ");
        String divider = scanner.nextLine();
        if (divider.isEmpty()) {
            divider = " "; // Устанавливаем пробел, если разделитель не был указан
            String transliteratedText = StringExtention.transliteration(inputText, " ");
        }
        String transliteratedText = StringExtention.transliteration(inputText, divider);
        System.out.println("Транслитерированный текст: " + transliteratedText);

        // Использование метода truncate
        System.out.println();
        System.out.println("Введите длину для усечения (по умолчанию 16): ");
        int length; // длина усечения
        while (true)
        {
            String lengthInput = scanner.nextLine(); // ввод значения длины

            // Если значение длины отрицательное число, то заменяем его на ноль
            int number = Integer.parseInt(lengthInput);
            if (number < 0) {
                lengthInput = "0";
            }

            if (lengthInput.isEmpty())
            {
                length = 16; // По умолчанию длина усечения 16 символов
                break;
            }
            else
            {
                try
                {
                    if (!lengthInput.contains("-"))
                    {
                        length = Integer.parseInt(lengthInput);
                        break;
                    }
                }
                catch (NumberFormatException e) // ошибка преобразования
                {
                    //System.out.println("Ошибка: введите только целое неотрицательное число.");
                }
                System.out.println("Ошибка: введите только целое неотрицательное число.");
            }
        }
        String truncatedText = StringExtention.truncate(inputText, length);
        System.out.println("Усеченный текст: " + truncatedText);

        scanner.close(); // закрывает объект Scanner и освобождает ресурсы, связанные с потоком ввода
    }
}
