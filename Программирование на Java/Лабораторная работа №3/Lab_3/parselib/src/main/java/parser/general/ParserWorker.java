package parser.general;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.nodes.Document;

import parser.OnCompleted;
import parser.OnNewDataHandler;
import parser.Parser;

public class ParserWorker<T> {
    private final Parser<T> parser;
    private Pages pages;
    private PageLoader loader;
    public List<OnNewDataHandler<T>> onNewDataList = new ArrayList<>();
    public List<OnCompleted> onCompletedList = new ArrayList<>();

    private boolean isActive;

    public ParserWorker(Parser<T> parser, Pages pages) {
        this(parser, new SimplePageLoader(), pages);
    }

    public ParserWorker(Parser<T> parser, PageLoader loader, Pages pages) {
        this.parser = parser;
        this.loader = loader;
        this.pages = pages;
    }

    public void setPages(Pages pages) {
        if (!isActive)
            this.pages = pages;
    }

    public void start() throws IOException {
        isActive = true;
        work();
    }

    public void abort() {
        isActive = false;
    }

    private void work() throws IOException {
        for (int i = 0; i < pages.getPagesCount(); i++) {
            if (!isActive) {
                emitCompleted();
                return;
            }

            Document doc = loader.load(pages.getPageRequest(i));
            T result = parser.Parse(doc, pages.getPageRequest(i));
            if (result != null)
                emitNewData(result);
        }

        emitCompleted();
        isActive = false;
    }

    private void emitCompleted() {
        for (OnCompleted onCompleted : onCompletedList) {
            onCompleted.onCompleted(this);
        }
    }

    private void emitNewData(T data) {
        for (OnNewDataHandler<T> onNewData : onNewDataList) {
            onNewData.onNewData(this, data);
        }
    }
}
