package parser.general;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class PageRequestSettings {
    private final String url;
    private final RequestMethod method;
    private final String requestBody;

    public PageRequestSettings(String url) {
        this.url = url;
        this.method = RequestMethod.GET;
        this.requestBody = null;
    }
}
