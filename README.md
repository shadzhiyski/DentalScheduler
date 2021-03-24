# Dental System

Provides basic functionalities for patient to request dental treatment.

## Description

There are two types of profiles - **Dentist** and **Patient**. Only a client/patient can register through the application. User profiles are provided by system administrator for dental workers.

* **Patient** - A client/patient can request treatment session with selected dental team.
* **Dental Worker** - Dental workers are grouped by dental teams. Each dental worker (dentist or assistant) in a dental team can access the requested treatment sessions and manage them.

### Test Data

In the current example project, user profiles are provided to test dental workers.

Test Dental Teams:

- **"Dental Team 01"**
  - Dentist: user: dentist_01@mail.com; password: Dentist_01#123456
  - Dentist Assistant: user: dentistAssistant_01@mail.com; password: DentistAssistant_01#123456
- **"Dental Team 01"**
  - Dentist: user: dentist_02@mail.com; password: Dentist_02#123456
  - Dentist Assistant: user: dentistAssistant_02@mail.com; password: DentistAssistant_02#123456

The provided treatment services by the dental teams are also added:
**"Bonding"
"Crowns and Caps"
"Filling And Repair"
"Extraction"
"Bridges and Implants"
"Braces"
"Teeth Whitening"
"Examination"**

All test data is generated on first launch of the back-end server.

## Back-End

The back-end of this project is written on .NET 5.0 using ASP.NET Core. To run the server you need to go to the **DentalSystem.Web.Api** directory and run the following script through the console:

```Shell
~/DentalSystem/DentalSystem.Web.Api$ dotnet build
~/DentalSystem/DentalSystem.Web.Api$ dotnet run
```

## Front-End

The **Vue** application is in the **DentalSystem.Web.UI** directory. npm package manager is used for this project. To start the app using the following script:

```Shell
~/DentalSystem/DentalSystem.Web.UI$ npm run serve
```

## Requirements

* .NET 5.0
* npm 6.14.5
* vue-cli 4.3.1