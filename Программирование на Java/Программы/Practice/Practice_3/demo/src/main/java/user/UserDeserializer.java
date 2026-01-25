package user;

import java.lang.reflect.Type;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

import lombok.var;

public class UserDeserializer implements JsonDeserializer<User> {
    @Override
    public User deserialize(JsonElement jsonEl, Type typeOfT, JsonDeserializationContext context)
            throws JsonParseException {
        var obj = jsonEl.getAsJsonObject();
        var nameObj = obj.getAsJsonObject("name");
        var locationObj = obj.getAsJsonObject("location");
        var streetObj = locationObj.getAsJsonObject("street");

        String firstName    = nameObj.get("first").getAsString();
        String lastName     = nameObj.get("last").getAsString();
        String email        = obj.get("email").getAsString();
        String phone        = obj.get("phone").getAsString();
        String photoUrl     = obj.getAsJsonObject("picture").get("large").getAsString();

        String country  = locationObj.get("country").getAsString();
        String state    = locationObj.get("state").getAsString();
        String city     = locationObj.get("city").getAsString();
        String street   = streetObj.get("name").getAsString();
        String building = streetObj.get("number").getAsString();
        Address address = new Address(country, state, city, street, building);

        return new User(firstName, lastName, address, email, phone, photoUrl);
    }
}
