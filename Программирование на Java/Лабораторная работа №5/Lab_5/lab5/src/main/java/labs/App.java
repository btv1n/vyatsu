package labs;

import com.google.gson.Gson;
import labs.habr.Article;
import labs.teatr.Play;
import labs.teatr.PlayWithDates;
import org.dom4j.*;
import org.dom4j.tree.BaseElement;
import org.xml.sax.SAXException;

import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.*;
import java.util.concurrent.ConcurrentHashMap;
import java.util.function.Function;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class App {
    static final String HABR_PATH = "./habr.json";
    static final String TEATR_PATH = "./teatr.json";

    public static void main(String[] args) throws IOException, SAXException {
        ex1();
        ex2();
        return;
    }

    private static void ex1() throws IOException, SAXException {
        var articles = getArticlesFromJson();
        var authorsGrouping = Arrays.stream(articles)
                .collect(Collectors.groupingBy(Article::getAuthor));

        saveXml(authorsGrouping, "author", "authorsGrouping.xml");

        List<Article> moreThen100Views = Arrays.stream(articles)
                .filter(article -> article.getViewsCount() > 100)
                .sorted(Comparator.comparing(Article::getTitle))
                .toList();

        saveXmlDOM4J(moreThen100Views, "moreThen100Views_dom4j.xml");
        saveXml(moreThen100Views, "moreThen100Views.xml");

        var distinctHubs = Arrays.stream(articles)
                .flatMap(article -> Arrays.stream(article.getHubs()))
                .distinct()
                .toList();
        saveXml(distinctHubs.stream().map(hub -> "<hub>" + hub + "</hub>"), "distinctHubs.xml");

        var avgReadingTime = Arrays.stream(articles)
                .mapToDouble(Article::getReadingTimeMinutes)
                .average()
                .getAsDouble();

        var readingTimeBelowAvg = Arrays.stream(articles)
                .filter(article -> article.getReadingTimeMinutes() < avgReadingTime)
                .toList();

        saveXmlDOM4J(readingTimeBelowAvg, "readingTimeBelowAvg.xml");
        return;
    }

    private static void ex2() throws IOException {
        var plays = getPlaysFromJson();

        var plays6DurationAsc = Arrays.stream(plays)
                .filter(play -> play.getAgeRestriction() == 6)
                .sorted(Comparator.comparing(Play::getDuration))
                .toList();

        saveXml(plays6DurationAsc, "plays6DurationAsc.xml");

        var playsDistinct = Arrays.stream(plays)
                .filter(distinctField(Play::getTitle))
                .toList();

        saveXml(playsDistinct, "playsDistinct.xml");

        var playsGrouping = Arrays.stream(plays).collect(Collectors.groupingBy(Play::getTitle));

        var playsWithDates = playsDistinct.stream()
                .map(
                        play ->
                                new PlayWithDates(
                                        play.getTitle(),
                                        playsGrouping.get(play.getTitle())
                                                .stream()
                                                .map(Play::getDate)
                                                .toList(),
                                        play.getDuration(),
                                        play.getAgeRestriction(),
                                        play.getPosterImageUrl()
                                )
                )
                .toList();
        saveXml(playsWithDates, "playsWithDates.xml");

        var playsNightly = Arrays.stream(plays)
                .filter(play -> {
                    int day = play.getDateCalendar().get(Calendar.DAY_OF_WEEK);
                    return day == Calendar.TUESDAY
                            || day == Calendar.THURSDAY
                            || day == Calendar.SATURDAY;
                })
                .filter(play -> play.getDuration() < 90)
                .toList();
        saveXml(playsNightly, "playsNightly.xml");

        return;
    }

    private static <T> Predicate<T> distinctField(Function<? super T, ?> fieldExtractor) {
        Set<Object> seen = ConcurrentHashMap.newKeySet();
        return t -> seen.add(fieldExtractor.apply(t));
    }

    private static void saveXml(Stream<String> tags, String path) throws IOException {
        FileWriter fw = new FileWriter(path);
        fw.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        fw.write("<root>");
        tags.forEach(tag -> {
            try {
                fw.write(tag);
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        });
        fw.write("</root>");
        fw.close();
    }

    private static void saveXml(List<? extends IXMLable> list, String path) throws IOException {
        saveXml(list.stream().map(IXMLable::toXml), path);
    }

    private static void saveXml(Map<String, ? extends List<? extends IXMLable>> m, String tagName, String path) throws IOException {
        FileWriter fw = new FileWriter(path);
        fw.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        fw.write("<root>");
        for (String key : m.keySet()) {
            fw.write("<" + tagName + " " + "key=\"" + key + "\">");
            for (IXMLable obj : m.get(key))
                fw.write(obj.toXml());
            fw.write("</" + tagName + ">");
        }
        fw.write("</root>");
        fw.close();
    }

    private static void saveXmlDOM4J(List<Article> articles, String path) throws SAXException, IOException {
        Document doc = DocumentHelper.createDocument();
        Element root = doc.addElement("root");

        for (Article article : articles) {
            addToElement(root, article);
        }

        FileWriter fw = new FileWriter(path);
        doc.write(fw);
        fw.close();
    }

    private static void addToElement(Element root, Article article) {
        Element articleElement = root.addElement("article");

        articleElement.addAttribute("views", article.getViews())
                .addAttribute("date", article.getDate().toString())
                .addAttribute("author", article.getAuthor())
                .addAttribute("readingtime", article.getReadingTime());


        Element hubsElement = articleElement.addElement("hubs");
        for (String hub : article.getHubs()) {
            hubsElement.addElement("hub").addText(hub);
        }

        articleElement.addElement("title").addText(article.getTitle());
        articleElement.addElement("previewImage").addText(article.getPreviewImageUrl());
        articleElement.addElement("preview").addText(article.getPreview());
    }

    private static Element makeNode(String name, String text) {
        return new BaseElement(name).addText(text);
    }

    private static Article[] getArticlesFromJson() throws IOException {
        String jsonContent = Files.readString(Paths.get(App.HABR_PATH));
        return new Gson().fromJson(jsonContent, Article[].class);
    }

    private static Play[] getPlaysFromJson() throws IOException {
        String jsonContent = Files.readString(Paths.get(App.TEATR_PATH));
        return new Gson().fromJson(jsonContent, Play[].class);
    }
}


class Articles {

}