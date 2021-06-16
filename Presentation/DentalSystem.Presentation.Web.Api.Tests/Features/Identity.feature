Feature: Login
    Test login functionality

    Scenario: Perform login
    When Login with user details
        | userName | password |
        | dentist_01@mail.com | Dentist_01#123456 |
        | dentist_02@mail.com | Dentist_02#123456 |
    Then Should receive access token