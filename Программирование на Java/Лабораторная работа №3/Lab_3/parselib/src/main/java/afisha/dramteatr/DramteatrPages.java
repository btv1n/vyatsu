package afisha.dramteatr;

import java.util.LinkedList;
import java.util.List;

import parser.general.PageRequestSettings;
import parser.general.Pages;

public class DramteatrPages implements Pages {
    public static final String BASE_URL = "https://kirovdramteatr.ru/";
    private final List<PageRequestSettings> pages = new LinkedList<>();

    public DramteatrPages() {
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
