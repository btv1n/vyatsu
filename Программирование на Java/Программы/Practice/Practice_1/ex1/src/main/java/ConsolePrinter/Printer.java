package ConsolePrinter;

public class Printer {
    public static void printLn(String s) {
        System.out.println(s);
    }

    public static void printLn(String s, Color c) {
        System.out.println(c + s + Color.ANSI_RESET);
    }
    
    public static void printLnErr(String s) {
        System.err.println(Color.ANSI_RED + s + Color.ANSI_RESET);
    }
    
    public static void printLnErr(String s, Color c) {
        System.err.println(c + s + Color.ANSI_RESET);
    }
}
