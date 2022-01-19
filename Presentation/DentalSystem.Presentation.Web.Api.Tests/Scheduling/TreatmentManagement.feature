Feature: Treatment Management
    Test Treatment Management feature

    Scenario: Get All treatments
    When Request all provided treatments by the system
    Then Should retrieve all provided treatments
        | Name | DurationInMinutes |
        | Bonding | 45 |
        | Crowns and Caps | 60 |
        | Filling And Repair | 45 |
        | Extraction | 60 |
        | Bridges and Implants | 120 |
        | Braces | 60 |
        | Teeth Whitening | 45 |
        | Examination | 30 |
