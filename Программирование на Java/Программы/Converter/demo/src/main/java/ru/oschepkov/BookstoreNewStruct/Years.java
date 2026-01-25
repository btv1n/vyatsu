package ru.oschepkov.BookstoreNewStruct;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@Builder
@NoArgsConstructor
@JacksonXmlRootElement(localName = " years")
public class Years {
    
    @JacksonXmlElementWrapper(useWrapping = false)
    @JsonProperty("year")
    private List<Year> years;
}
