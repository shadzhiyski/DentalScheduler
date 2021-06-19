Feature: Profile Management
    Test Profile Management feature

    Scenario: Update profile
    Given Authorized user with auth token
        | userName | password |
        | dentist_01@mail.com | Dentist_01#123456 |
        | dentist_02@mail.com | Dentist_02#123456 |
    When Update profile with valid details
        | firstName | lastName |
        | Dental 2 | Worker 2 |
        | Dental 3 | Worker 3 |
    Then Should update profile info successfully
    When Get profile info
    Then Should get updated profile info