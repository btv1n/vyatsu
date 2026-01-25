package user;

import java.io.IOException;
import java.io.StringReader;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpRequest.Builder;
import java.time.Duration;
import java.util.LinkedList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import lombok.var;

public class UserApiService {
    private URI uri;
    private HttpClient httpClient = HttpClient.newHttpClient();
    private Builder httpRequestBuilder;
    private Gson gson;
    private DocumentBuilder documentBuilder;

    public UserApiService() throws ParserConfigurationException {
        this("https://randomuser.me/api/");
    }

    public UserApiService(String uri) throws ParserConfigurationException {
        GsonBuilder gsonBldr = new GsonBuilder();
        gsonBldr.registerTypeAdapter(User.class, new UserDeserializer());
        gsonBldr.registerTypeAdapter(User[].class, new ResponseDeserializer());

        documentBuilder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
        gson = gsonBldr.create();

        this.uri = URI.create(uri);
        httpClient = HttpClient.newHttpClient();
        this.httpRequestBuilder = HttpRequest
                .newBuilder()
                .timeout(Duration.ofSeconds(15));
    }

    public User[] getUsersJson(int count) throws IOException, InterruptedException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("?results=" + Integer.toString(count)))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());

        return gson.fromJson(response.body(), User[].class);
    }

    public User[] getUsersXml(int count) throws IOException, InterruptedException, SAXException {
        var request = httpRequestBuilder
                .uri(this.uri.resolve("?format=xml&results=" + Integer.toString(count)))
                .GET()
                .build();
        var response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
        return parseXmlResponse(response.body());
    }

    private User[] parseXmlResponse(String xml) throws IOException, SAXException {
        List<User> users = new LinkedList<>();
        // System.out.println(xml);
        Document doc = documentBuilder.parse(new InputSource(new StringReader(xml)));
        NodeList userNodes = doc.getElementsByTagName("results");
        for (int i = 0; i < userNodes.getLength() - 1; i++) {
            users.add(parseXmlNode((Element) userNodes.item(i)));
        }
        return users.toArray(new User[0]);
    }

    private User parseXmlNode(Element userNode) throws IOException {
        var nameNode = (Element) userNode.getElementsByTagName("name").item(0);
        var locationNode = (Element) userNode.getElementsByTagName("location").item(0);
        var streetNode = (Element) locationNode.getElementsByTagName("street").item(0);

        String firstName = nameNode.getElementsByTagName("first").item(0).getTextContent();
        String lastName = nameNode.getElementsByTagName("last").item(0).getTextContent();
        String email = userNode.getElementsByTagName("email").item(0).getTextContent();
        String phone = userNode.getElementsByTagName("phone").item(0).getTextContent();
        String photoUrl = ((Element) userNode.getElementsByTagName("picture").item(0)).getElementsByTagName("large")
                .item(0).getTextContent();

        String country = locationNode.getElementsByTagName("country").item(0).getTextContent();
        String state = locationNode.getElementsByTagName("state").item(0).getTextContent();
        String city = locationNode.getElementsByTagName("city").item(0).getTextContent();
        String street = streetNode.getElementsByTagName("name").item(0).getTextContent();
        String building = locationNode.getElementsByTagName("number").item(0).getTextContent();
        Address address = new Address(country, state, city, street, building);

        return new User(firstName, lastName, address, email, phone, photoUrl);
    }
}
