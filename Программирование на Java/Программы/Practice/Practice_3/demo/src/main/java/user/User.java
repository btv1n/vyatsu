package user;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class User {
    String firstName;
    String lastName;
    Address address;
    String email;
    String phone;
    String photoUrl;
}
