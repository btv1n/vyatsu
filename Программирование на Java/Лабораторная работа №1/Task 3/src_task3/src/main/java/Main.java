package main.java;
import java.util.Scanner;
import java.lang.String;
import main.java.extensions.TextFormatting;

public class Main
{
    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);

        // Запрос текста у пользователя
        System.out.println("Введите текст:");
        String text = scanner.nextLine();

        // Инструкции по выбору способа изменения регистра
        System.out.println("Выберите способ изменения регистра текста:");
        System.out.println("(1) Как в предложениях");
        System.out.println("(2) все строчные");
        System.out.println("(3) ВСЕ ПРОПИСНЫЕ");
        System.out.println("(4) Начинать С Прописных");
        System.out.println("(5) иЗМЕНИТЬ РЕГИСТР");

        int option;
        // Считывание выбора пользователя
        String input;

        // Переменная для хранения результата
        String result;

        while (true)
        {
            input = scanner.nextLine();

            try
            {
                option = Integer.parseInt(input);
                if (option >= 1 && option <= 5)
                    break;
            }
            catch (NumberFormatException e)
            {

            }
            System.out.println("Ошибка: ведите правильный номер опции");
        }

        // Выбор метода в зависимости от опции пользователя
        switch (option)
        {
            case 1:
                result = TextFormatting.toSentenceCase(text); // Метод для формата "Как в предложениях"
                break;
            case 2:
                result = TextFormatting.toLowerCase(text); // Метод для формата "все строчные"
                break;
            case 3:
                result = TextFormatting.toUpperCase(text); // Метод для формата "ВСЕ ПРОПИСНЫЕ"
                break;
            case 4:
                result = TextFormatting.toTitleCase(text); // Метод для формата "Начинать С Прописных"
                break;
            case 5:
                result = TextFormatting.swapCase(text); // Метод для "иЗМЕНИТЬ РЕГИСТР"
                break;
            default:
                result = "Такой опции не существует"; // Обработка неверного ввода
                break;
        }

        // Вывод результата
        System.out.println("Результат:");
        System.out.println(result);
    }
}
