package afisha.kukla;

import java.util.LinkedList;
import java.util.List;

import parser.general.PageRequestSettings;
import parser.general.Pages;

public class KuklaPages implements Pages {
    public static final String BASE_URL = "https://kirovkukla.ru/";
    public static final String AJAX_CHUNK_URL = BASE_URL + "assets/components/ajaxchunk/connector.php";
    private final List<PageRequestSettings> pages = new LinkedList<>();

    public KuklaPages() {
        pages.add(new PageRequestSettings(BASE_URL + "afisha"));
    }

    @Override
    public int getPagesCount() {
        return pages.size();
    }

    @Override
    public PageRequestSettings getPageRequest(int i) {
        return pages.get(i);
    }

    public void addPage(PageRequestSettings page) {
        this.pages.add(page);
    }
}
