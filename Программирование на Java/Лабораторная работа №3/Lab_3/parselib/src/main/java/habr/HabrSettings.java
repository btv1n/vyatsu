package habr;

import parser.pages.PagesParserSettings;

public class HabrSettings extends PagesParserSettings {
    public HabrSettings(int start,  int end) {
        startPage = start;
        endPage = end;
        baseUrl = "https://habr.com/ru/articles";
        suffix = "page{page}";
    }
}
