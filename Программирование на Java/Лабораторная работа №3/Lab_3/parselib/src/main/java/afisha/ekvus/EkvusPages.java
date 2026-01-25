package afisha.ekvus;

import parser.general.PageRequestSettings;
import parser.general.Pages;

public class EkvusPages implements Pages {
    public static final String BASE_URL = "https://ekvus-kirov.ru/";

    @Override
    public int getPagesCount() {
        return 1;
    }

    @Override
    public PageRequestSettings getPageRequest(int i) {
        return new PageRequestSettings(BASE_URL + "afisha");
    }
}
