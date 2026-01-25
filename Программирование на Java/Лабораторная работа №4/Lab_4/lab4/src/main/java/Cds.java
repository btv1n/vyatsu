
import java.time.Duration;

import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.FluentWait;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.Wait;

public class Cds {
    final WebDriver driver;
    final String URL = "https://m.cdsvyatka.com/";
    final String CITY = "1";
    final String ROUTE = "1087";
    final String STATION = "175";

    public Cds(WebDriver driver) {
        this.driver = driver;
    }

    public void openMapScenario() {
        driver.get(URL);
        selectCity();
        selectRoute();
        click(By.xpath("//input[@id=\"id_buttonMap\"]"));

        try {
            Thread.sleep(500);
        } catch (Exception e) {
        } finally {
        }

        driver.navigate().refresh();
    }

    public void getTimetableScenario() {
        driver.get(URL);

        selectCity();
        selectRoute();
        selectStation();

        click(By.xpath("//input[@id=\"id_buttonRaspis\"]"));
    }

    public void getBusInfoScenario() {
        openMapScenario();

        By busBy = By.xpath("(//canvas[contains(@class, 'leaflet-clickable')])[2]");

        Wait<WebDriver> wait = new FluentWait<>(driver).withTimeout(Duration.ofSeconds(1));
        wait.until(d -> {
            return !d.findElements(busBy).isEmpty();
        });

        click(busBy);

        WebElement popup = driver.findElement(By.xpath("//div[@class='leaflet-popup-content']"));
        System.out.println(popup.getText());
    }

    private void selectCity() {
        select(By.xpath("//select[@id=\"id_CitySelect\"]"), CITY);
    }

    private void selectRoute() {
        select(By.xpath("//select[@id=\"id_MarshSelect\"]"), ROUTE);
    }

    private void selectStation() {
        select(By.xpath("//select[@id=\"id_StopSelect\"]"), STATION);
    }

    private void select(By selectBy, String value) {
        var selectElement = driver.findElement(selectBy);
        var select = new Select(selectElement);
        select.selectByValue(value);
    }

    private void click(By elementBy) {
        var btn = driver.findElement(elementBy);
        ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true)", btn);
        btn.click();
    }
}
