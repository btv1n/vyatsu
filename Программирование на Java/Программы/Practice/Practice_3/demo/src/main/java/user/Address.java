package user;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Address {
    String country;
    String state;
    String city;
    String street;
    String building;
}
