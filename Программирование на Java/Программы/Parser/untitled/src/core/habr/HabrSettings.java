package core.habr;

import core.ParserSettings;

public class HabrSettings extends ParserSettings {

    public HabrSettings(int start, int end) {
        startPoint = start;
        endPoint = end;
        BASE_URL = "https://habr.com/ru/all";
        PREFIX = "page{CurrentId}";
    }
}

