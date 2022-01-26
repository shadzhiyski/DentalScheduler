Feature: Treatment Session Management
    Test Treatment Session Management feature

    Scenario: Request treatment session
    Given Register client "patient_tsm_1@mail.com" with password "Patient_01#123456"
    And Patient ReferenceId of authorized user
    And Dental team ReferenceId of "DentalTeam 01"
    And Treatment ReferenceId of "Bonding"
    And Treatment session details "2020-06-21T09:30:00Z" "2020-06-21T10:15:00Z"
    When Request treatment session
    Then Should create treatment session successfully

    Scenario: Request treatment session overlapping with existing and different dental team
    Given Register client "patient_tsm_2@mail.com" with password "Patient_01#123456"
    And Patient ReferenceId of authorized user
    And Have requested treatment session for "2020-06-22T09:30:00Z" "2020-06-22T10:15:00Z", treatment "Crowns and Caps" and dental team "DentalTeam 01"
    And Dental team ReferenceId of "DentalTeam 02"
    And Treatment ReferenceId of "Bonding"
    And Treatment session details "2020-06-22T10:00:00Z" "2020-06-22T10:30:00Z"
    When Request treatment session
    Then Should fail to create treatment session

    Scenario: Request treatment session overlapping with existing one
    Given Register client "patient_tsm_3@mail.com" with password "Patient_01#123456"
    And Patient ReferenceId of authorized user
    And Have requested treatment session for "2020-06-23T09:30:00Z" "2020-06-23T10:15:00Z", treatment "Crowns and Caps" and dental team "DentalTeam 01"
    And Dental team ReferenceId of "DentalTeam 01"
    And Treatment ReferenceId of "Bonding"
    And Treatment session details "2020-06-23T10:00:00Z" "2020-06-23T10:30:00Z"
    When Request treatment session
    Then Should fail to create treatment session