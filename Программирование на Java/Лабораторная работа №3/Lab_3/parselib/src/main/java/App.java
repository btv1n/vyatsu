import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

import com.google.gson.Gson;

import afisha.dramteatr.DramteatrPages;
import afisha.dramteatr.DramteatrParser;
import afisha.ekvus.EkvusPages;
import afisha.ekvus.EkvusParser;
import afisha.kukla.KuklaPageLoader;
import afisha.kukla.KuklaPages;
import afisha.kukla.KuklaParser;
import afisha.structs.Play;
import habr.HabrParser;
import habr.HabrSettings;
import habr.structs.Article;
import parser.OnCompleted;
import parser.OnNewDataHandler;
import parser.general.ParserWorker;
import parser.pages.PagesParserWorker;

public class App {
    private static final int START = 1;
    private static final int END = 5;

    public static void main(String[] args) {
        evkus();
        dramteatr();
        kukla();
        // habr();
    }

    private static void evkus() {
        var pages = new EkvusPages();
        ParserWorker<List<Play>> parser = new ParserWorker<>(new EkvusParser(), pages);

        parser.onCompletedList.add((OnCompleted) (Object sender) -> {
            System.out.println("OnCompleted");
        });

        List<Play> list = new ArrayList<>();
        parser.onNewDataList.add((OnNewDataHandler<List<Play>>) (Object sender, List<Play> e) -> {
            list.addAll(e);
        });

        try {
            parser.start();
        } catch (IOException e1) {
            e1.printStackTrace();
        }

        Gson gson = new Gson();

        try {
            Files.write(Paths.get("./result_evkus.json"), gson.toJson(list).getBytes(StandardCharsets.UTF_8));
        } catch (IOException e1) {
            e1.printStackTrace();
        }
    }

    private static void dramteatr() {
        var pages = new DramteatrPages();
        ParserWorker<List<Play>> parser = new ParserWorker<>(new DramteatrParser(pages), pages);

        parser.onCompletedList.add((OnCompleted) (Object sender) -> {
            System.out.println("OnCompleted");
        });

        List<Play> list = new ArrayList<>();
        parser.onNewDataList.add((OnNewDataHandler<List<Play>>) (Object sender, List<Play> e) -> {
            list.addAll(e);
        });

        try {
            parser.start();
        } catch (IOException e1) {
            e1.printStackTrace();
        }

        Gson gson = new Gson();

        try {
            Files.write(Paths.get("./result_dramteatr.json"), gson.toJson(list).getBytes(StandardCharsets.UTF_8));
        } catch (IOException e1) {
            e1.printStackTrace();
        }
    }

    private static void kukla() {
        var pages = new KuklaPages();
        ParserWorker<List<Play>> parser = new ParserWorker<>(new KuklaParser(pages), new KuklaPageLoader(), pages);

        parser.onCompletedList.add((OnCompleted) (Object sender) -> {
            System.out.println("OnCompleted");
        });

        List<Play> list = new ArrayList<>();
        parser.onNewDataList.add((OnNewDataHandler<List<Play>>) (Object sender, List<Play> e) -> {
            list.addAll(e);
        });

        try {
            parser.start();
        } catch (IOException e1) {
            e1.printStackTrace();
        }

        Gson gson = new Gson();

        try {
            Files.write(Paths.get("./result.json"),
                    gson.toJson(list).getBytes(StandardCharsets.UTF_8));
        } catch (IOException e1) {
            e1.printStackTrace();
        }
    }

    private static void habr() {
        PagesParserWorker<List<Article>> parser = new PagesParserWorker<>(new HabrParser());
        parser.setParserSettings(new HabrSettings(START, END));
        parser.onCompletedList.add((OnCompleted) (Object sender) -> {
            System.out.println("OnCompleted");
        });

        List<Article> list = new ArrayList<>();

        parser.onNewDataList.add((OnNewDataHandler<List<Article>>) (Object sender, List<Article> e) -> {
            list.addAll(e);
        });

        try {
            parser.start();
        } catch (IOException e1) {
            e1.printStackTrace();
        }

        Gson gson = new Gson();

        try {
            Files.write(Paths.get("./result_habr.json"), gson.toJson(list).getBytes(StandardCharsets.UTF_8));
        } catch (IOException e1) {
            e1.printStackTrace();
        }
    }
}
