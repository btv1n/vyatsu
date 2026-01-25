package parser.pages;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.nodes.Document;

import parser.OnCompleted;
import parser.OnNewDataHandler;
import parser.Parser;

public class PagesParserWorker<T> {
    private final Parser<T> parser;
    private PagesParserSettings parserSettings;
    private PagesHtmlLoader loader;
    public List<OnNewDataHandler<T>> onNewDataList = new ArrayList<>();
    public List<OnCompleted> onCompletedList = new ArrayList<>();

    private boolean isActive;

    public PagesParserWorker(Parser<T> parser) {
        this.parser = parser;
    }

    public void setParserSettings(PagesParserSettings parserSettings) {
        loader = new PagesHtmlLoader(parserSettings);
        this.parserSettings = parserSettings;
    }

    public void start() throws IOException {
        isActive = true;
        work();
    }

    public void abort() {
        isActive = false;
    }

    private void work() throws IOException {
        for (int i = parserSettings.getStartPage(); i <= parserSettings.getEndPage(); i++) {
            if (!isActive) {
                onCompletedList.get(0).onCompleted(this);
                return;
            }

            Document doc = loader.load(i);
            T result = parser.Parse(doc, null);
            onNewDataList.get(0).onNewData(this, result);
        }

        onCompletedList.get(0).onCompleted(this);
        isActive = false;
    }
}
