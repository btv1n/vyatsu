package services.json;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;

public class JacksonService {
    private final static ObjectMapper mapper = new ObjectMapper();
    static {
        // config mapper
        mapper.enable(SerializationFeature.WRITE_DATES_AS_TIMESTAMPS);
    }
    
    public static String toJsonString(Object obj) throws JsonProcessingException{
        return mapper.writeValueAsString(obj);
    }
}
