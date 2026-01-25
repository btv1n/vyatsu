package parser.pages;

import lombok.Getter;

@Getter
public abstract class PagesParserSettings {
    public String baseUrl;
    public String suffix;
    protected int startPage;
    protected int endPage;
}
