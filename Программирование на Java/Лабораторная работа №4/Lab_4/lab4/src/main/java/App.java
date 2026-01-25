
import java.io.File;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.service.DriverFinder;

public class App {
    public static void main(String[] args) {
        //cds();

        otzovik();
    }

    private static void cds() {
        WebDriver driver = getChromeDriver();
        Cds cds = new Cds(driver);

        /*runScenario(() -> {
            cds.openMapScenario();
        });

        runScenario(() -> {
            cds.getTimetableScenario();
        });*/

        runScenario(() -> {
            cds.getBusInfoScenario();
        });

        driver.quit();
    }

    private static void otzovik() {
        WebDriver driver = getChromeDriver();

        
        Otzovik otzovik = new Otzovik(driver);
        runScenario(() -> {
            otzovik.writeReviewScenario();
        });

        driver.quit();
    }

    private static ChromeDriver getChromeDriver() {
        ChromeOptions options = new ChromeOptions();
        options.setBinary(getChromeLocation());
        return new ChromeDriver(options);
    }

    private static File getChromeLocation() {
        ChromeOptions options = new ChromeOptions();
        options.setBrowserVersion("stable");
        DriverFinder finder = new DriverFinder(ChromeDriverService.createDefaultService(), options);
        return new File(finder.getBrowserPath());
    }

    private static void runScenario(Runnable runnable) {
        runnable.run();
        try {
            Thread.sleep(5000);
        } catch (Exception e) {
        } finally {
        }
    }
}
