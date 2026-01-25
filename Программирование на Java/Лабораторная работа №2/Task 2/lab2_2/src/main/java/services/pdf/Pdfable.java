package services.pdf;

import com.lowagie.text.Document;

public interface Pdfable {
    public void toPdf(Document doc);
}
