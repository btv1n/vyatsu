package services.json;

import org.json.simple.JSONObject;

public interface Jsonable {
    public default String toJson() {
        return this.toJsonObject().toJSONString();
    }

    public JSONObject toJsonObject();
}
