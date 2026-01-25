package core;

import core.habr.HabrParser;
import core.habr.HabrSettings;

import java.io.IOException;
import java.util.ArrayList;

public class Program {
    static ArrayList<String> news = new ArrayList<>();

    public static void main(String[] args) {
        ParserWorker<ArrayList<String>>  parser = new ParserWorker<>(new HabrParser());

        // задайте значения переменным start и end
        int start = 1;
        int end = 2;
        parser.setParserSettings(new HabrSettings(start, end));
        parser.onCompletedList.add(new Completed());
        parser.onNewDataList.add(new NewData());
        try {
            parser.Start();
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
            Thread.sleep(10000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        parser.Abort();
    }

    static class NewData implements ParserWorker.OnNewDataHandler<ArrayList<String>> {

        @Override
        public void OnNewData(Object sender, ArrayList<String> args) {
            for (String s : args) {
                System.out.println(s);
            }
        }
    }

    static class Completed implements ParserWorker.OnCompleted {

        @Override
        public void OnCompleted(Object sender) {
            System.out.println("Загрузка закончена");
        }
    }

}
