package ru.oschepkov;

public class App {

    public static void main(String[] args) {
        TypeSpecifier typeSpecifier = new TypeSpecifier();
        try {
            switch (typeSpecifier.apply(args[0])) {
                case XML: {
                    (new BookstoreConverterToJson()).convert(args[0], args[1]); break;
                }
                case JSON: {
                    (new BookstoreConverterToXml()).convert(args[1], args[0]); break;
                } 
                default: {
                    System.out.println("Invalid input"); break;
                }
            }   
        }
        catch (Exception e) {
            System.out.println("Непредвиденная ошибка\n" + e.toString());
        }
    }
    
}
