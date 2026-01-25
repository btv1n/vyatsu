package user;

import java.lang.reflect.Type;
import java.util.LinkedList;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

import lombok.var;

public class ResponseDeserializer implements JsonDeserializer<User[]> {
    @Override
    public User[] deserialize(JsonElement jsonEl, Type typeOfT, JsonDeserializationContext context)
            throws JsonParseException {
        LinkedList<User> users = new LinkedList<>();
        var obj = jsonEl.getAsJsonObject();
        obj.get("results").getAsJsonArray().forEach(e -> users.add(context.deserialize(e, User.class)));
        return users.toArray(new User[0]);
    }
}
