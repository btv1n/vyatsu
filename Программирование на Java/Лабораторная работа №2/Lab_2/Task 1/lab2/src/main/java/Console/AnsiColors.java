package Console;

// escape-коды:
// [0m - код сброса
// [31m - код красного цвета
// [36m - код бирюзового цвета
// final - запрещает изменять значения после инициализации

public class AnsiColors {
    public static final String ANSI_RESET = "\u001B[0m";
    public static final String ANSI_RED = "\u001B[31m";
    public static final String ANSI_CYAN = "\u001B[36m";
}
