import java.io.File;

import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.service.DriverFinder;

public class AppTest {
    WebDriver driver;
    final String URL = "https://irr.ru/";

    private static File getChromeLocation() {
        ChromeOptions options = new ChromeOptions();
        options.setBrowserVersion("stable");
        DriverFinder finder = new DriverFinder(ChromeDriverService.createDefaultService(), options);
        return new File(finder.getBrowserPath());
    }

    @Before
    public void before() {
        ChromeOptions options = new ChromeOptions();
        options.setBinary(getChromeLocation());
        driver = new ChromeDriver(options);
    }

    @Test
    public void titleTest() {
        driver.get(URL);

        Assert.assertEquals("", driver.getTitle());
    }

    @After
    public void after() {
        driver.quit();
    }
}
