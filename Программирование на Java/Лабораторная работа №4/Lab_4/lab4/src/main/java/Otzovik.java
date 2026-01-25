import java.time.Duration;

import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.FluentWait;
import org.openqa.selenium.support.ui.Wait;

public class Otzovik {
    final WebDriver driver;
    final String URL = "https://otzovik.com/";

    public Otzovik(WebDriver driver) {
        this.driver = driver;
    }

    public void writeReviewScenario() {
        driver.get(URL);
        click(By.xpath("//a[contains(@class, 'header-postreview-btn')]"));
        input(By.xpath("//input[@id=\"tproduct\"]"), "Ford Focus 2 хэтчбек");

        By prodSelect = By.xpath("(//div[contains(@class, 'prod-popup')]/a[contains(@class, 'popup-obj')])[1]");

        Wait<WebDriver> wait = new FluentWait<>(driver).withTimeout(Duration.ofSeconds(1));
        wait.until(d -> {
            return !d.findElements(prodSelect).isEmpty();
        });
        click(prodSelect);

        click(By.xpath("//div[contains(@class, 'rating-block')]//div[contains(@class, 'rating')]/*[5]"));
        input(By.xpath("//input[@id='content_title']"), "Заголовок");
        input(By.xpath("//textarea[contains(@class, 'text_area_auto_height_ta')]"),
                "Прежде всего, глубокий уровень погружения в значительной степени обусловливает важность инновационных методов управления процессами. Разнообразный и богатый опыт говорит нам, что современная методология разработки обеспечивает широкому кругу (специалистов) участие в формировании форм воздействия. Как уже неоднократно упомянуто, активно развивающиеся страны третьего мира являются только методом политического участия и своевременно верифицированы.");
        input(By.xpath("//textarea[@name='content_pros']"), "Хорошо");
        input(By.xpath("//textarea[@name='content_cons']"), "Плохо");

        click(By.xpath("//div[contains(@class, 'recommend-block')]//label[1]"));

        click(By.xpath("//div[@id='wayofac']//label[1]"));

        click(By.xpath("//button[@id='previewbtn']"));
    }

    private void input(By inputBy, String value) {
        var inputElement = driver.findElement(inputBy);
        inputElement.sendKeys(value);
    }

    private void click(By elementBy) {
        var btn = driver.findElement(elementBy);
        ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true)", btn);
        btn.click();
    }
}
