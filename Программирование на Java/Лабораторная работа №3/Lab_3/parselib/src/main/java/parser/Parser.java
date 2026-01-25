package parser;

import org.jsoup.nodes.Document;

import parser.general.PageRequestSettings;

public interface Parser<T> {
    T Parse(Document doc, PageRequestSettings settings);
}
