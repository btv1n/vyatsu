package core;

import org.jsoup.nodes.Document;

public interface Parser<T> {
    T Parse(Document document);
}
