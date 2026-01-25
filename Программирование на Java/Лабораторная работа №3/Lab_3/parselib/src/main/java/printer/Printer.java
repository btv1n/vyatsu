package printer;

public class Printer {
    public static void printLn(String s) {
        System.out.println(s);
    }

    public static void printLn(String s, AnsiColor c) {
        System.out.println(c + s + AnsiColor.RESET);
    }
    
    public static void printLnErr(String s) {
        System.err.println(AnsiColor.RED + s + AnsiColor.RESET);
    }
    
    public static void printLnErr(String s, AnsiColor c) {
        System.err.println(c + s + AnsiColor.RESET);
    }
}
