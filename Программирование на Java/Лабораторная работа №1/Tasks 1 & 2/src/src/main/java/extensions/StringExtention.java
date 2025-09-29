package main.java.extensions;

import java.util.HashMap;
import java.util.Map;

public class StringExtention
{
    /*
    Ключ: Character — символ (буква).
    Значение: String — строка (латинский эквивалент символа).
    Используется HashMap<>, который является реализацией интерфейса Map и хранит пары "ключ-значение".
    private — переменная доступна только в текущем классе.
    static — переменная принадлежит классу, а не объектам класса.
    final — переменная не может быть изменена после инициализации.
    Map<Character, String> — интерфейс, который описывает коллекцию, в которой каждый элемент представляет собой пару "ключ-значение".
    new HashMap<>() — создание нового объекта типа HashMap, который будет хранить данные.
    */
    private static final Map<Character, String> myDictionary = new HashMap<>();
    static
    {
        myDictionary.put('а', "a");  myDictionary.put('б', "b");  myDictionary.put('в', "v");
        myDictionary.put('г', "g");  myDictionary.put('д', "d");  myDictionary.put('е', "e");
        myDictionary.put('ё', "e");  myDictionary.put('ж', "zh"); myDictionary.put('з', "z");
        myDictionary.put('и', "i");  myDictionary.put('й', "i");  myDictionary.put('к', "k");
        myDictionary.put('л', "l");  myDictionary.put('м', "m");  myDictionary.put('н', "n");
        myDictionary.put('о', "o");  myDictionary.put('п', "p");  myDictionary.put('р', "r");
        myDictionary.put('с', "s");  myDictionary.put('т', "t");  myDictionary.put('у', "u");
        myDictionary.put('ф', "f");  myDictionary.put('х', "h");  myDictionary.put('ц', "c");
        myDictionary.put('ч', "ch"); myDictionary.put('ш', "sh"); myDictionary.put('щ', "sh'");
        myDictionary.put('ъ', "");   myDictionary.put('ы', "i");  myDictionary.put('ь', "");
        myDictionary.put('э', "e");  myDictionary.put('ю', "yu"); myDictionary.put('я', "ya");

        myDictionary.put('А', "A");  myDictionary.put('Б', "B");  myDictionary.put('В', "V");
        myDictionary.put('Г', "G");  myDictionary.put('Д', "D");  myDictionary.put('Е', "E");
        myDictionary.put('Ё', "E");  myDictionary.put('Ж', "Zh"); myDictionary.put('З', "Z");
        myDictionary.put('И', "I");  myDictionary.put('Й', "I");  myDictionary.put('К', "K");
        myDictionary.put('Л', "L");  myDictionary.put('М', "M");  myDictionary.put('Н', "N");
        myDictionary.put('О', "O");  myDictionary.put('П', "P");  myDictionary.put('Р', "R");
        myDictionary.put('С', "S");  myDictionary.put('Т', "T");  myDictionary.put('У', "U");
        myDictionary.put('Ф', "F");  myDictionary.put('Х', "H");  myDictionary.put('Ц', "C");
        myDictionary.put('Ч', "Ch"); myDictionary.put('Ш', "Sh"); myDictionary.put('Щ', "Sh'");
        myDictionary.put('Ъ', "");   myDictionary.put('Ы', "I");  myDictionary.put('Ь', "");
        myDictionary.put('Э', "E");  myDictionary.put('Ю', "Yu"); myDictionary.put('Я', "Ya");
    }

    public static String transliteration(String input, String divider)
    {
        if (divider == null) divider = " "; // Устанавливаем пробел, если разделитель не указан
        StringBuilder result = new StringBuilder(); // StringBuilder используется для создания строки без постоянного создания новых объектов (эффективно при конкатенации строк).

        // Цикл проходится по каждому элементу в строке
        // input.toLowerCase(): Преобразует строку input в нижний регистр, чтобы транслитерировать символы без учета их регистра.
        // toCharArray(): Преобразует строку в массив символов, чтобы можно было пройтись по каждому символу.
        for (char c : input.toCharArray())
        {
            // Для каждого символа c в строке ищется его транслитерированный эквивалент в словаре
            // myDictionary. Если символ не найден в словаре, используется сам символ как есть
            // (через String.valueOf(c))
            String latin = myDictionary.getOrDefault(c, String.valueOf(c));
            result.append(latin); // Добавляет найденный латинский эквивалент или сам символ в StringBuilder
        }

        // toString() конвертирует StringBuilder в обычную строку
        // replace(" ", divider): Заменяет все пробелы на указанный пользователем разделитель
        return result.toString().replace(" ", divider);
    }

    // Метод с параметрам по умолчинию - divider = " "
    public static String transliteration(String input)
    {
        return transliteration(input, " ");
    }


    /*
public static String truncate(String input, int length)
{
    if (length <= 0) return "";
    if (input.length() <= length) return input; // если длина исходной строки меньше или равна указанной длине, то усечение не требуется

    String truncated = input.substring(0, length); // извлекает подстроку из первой части строки input длиной
                                                   // substring(0, length) используется для извлечения части строки, начиная с позиции 0 (первого символа) и заканчивая позицией length (не включая ее).
    if (truncated.endsWith(" ")) // если усеченная строка заканчивается пробелом
    {
        truncated = truncated.trim(); // пробел удаляется
    }
    return truncated + "...";
}
*/
    public static String truncate(String input, int length)
    {
        if (length <= 0) return "";
        if (input.length() <= length) return input; // если длина исходной строки меньше или равна указанной длине, то усечение не требуется

        String truncated = input.substring(0, length); // извлекает подстроку из первой части строки input длиной
        // substring(0, length) используется для извлечения части строки, начиная с позиции 0 (первого символа) и заканчивая позицией length (не включая ее).

        int indexSymbols = 0;
        char[] charArray = input.toCharArray();
        for (int i = length; i < charArray.length; i++)
        {
            char c = charArray[i];
            if (c != ' ')
                indexSymbols = i;
        }


        // Проверяем на наличие иных символов, кроме пробела исходную строку
        boolean isNotSymbolsInput = false;
        for (char c : input.toCharArray()) // проверка наличия иных символов, кроме пробела
        {
            if (c != ' ')
                isNotSymbolsInput = true;
        }

        if (!isNotSymbolsInput) // если строка ссстоит полность из пробелов
            return "";


        char[] charArray2 = truncated.toCharArray();
        if (truncated.endsWith(" ")) // если усеченная строка заканчивается пробелом
        {
            truncated = truncated.trim(); // пробел удаляется
        }
        // assertEquals("...", StringExtention.truncate("    А", 4));
        if (length < indexSymbols) // если после обрезанной строки есть символы добавлем ...
            return truncated + "...";

        boolean isNotSymbols = false;
        for (char c : truncated.toCharArray()) // проверка наличия иных символов, кроме пробела
        {
            if (c != ' ')
                isNotSymbols = true;
        }

        if (!isNotSymbols) // если строка ссстоит полность из пробелов
            return "...";

        // удаляет лишние пробелы
        return truncated.replaceAll("\\s+$", "");
    }

    // Метод для реализации параметра по умолчанию
    public static String truncate(String input)
    {
        return truncate(input, 16);
    }
}
