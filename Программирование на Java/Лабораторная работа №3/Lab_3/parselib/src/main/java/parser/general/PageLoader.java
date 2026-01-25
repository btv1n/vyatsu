package parser.general;

import java.io.IOException;

import org.jsoup.nodes.Document;

public interface PageLoader {
    public Document load(PageRequestSettings page) throws IOException;
}
