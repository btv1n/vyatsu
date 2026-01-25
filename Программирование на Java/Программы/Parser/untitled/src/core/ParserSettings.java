package core;

public abstract class ParserSettings {

    // Адрес сайта
    public static String BASE_URL;

    // префих страницы
    public static String PREFIX;

    // начало пагинации
    protected int startPoint;

    // конец пагинации
    protected int endPoint;

    public int getStartPoint() {
        return startPoint;
    }

    public int getEndPoint() {
        return endPoint;
    }
}

