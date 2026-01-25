package services.json;

import org.json.simple.JSONArray;

import com.fasterxml.jackson.core.JsonProcessingException;

public class SimpleJSONService {
    @SuppressWarnings("unchecked")
    public static String toJsonString(Jsonable[] arr) throws JsonProcessingException{
        JSONArray jsonArr = new JSONArray();

        for (Jsonable e : arr) {
            jsonArr.add(e.toJsonObject());
        }

        return jsonArr.toJSONString();
    }
}