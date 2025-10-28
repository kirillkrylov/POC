---
_layout: landing
---

# How we test

This project uses a combination of unit testing and end-to-end (E2E) testing to ensure code quality and reliability.

Unit testing is fast and offers immediate feedback, allowing developers to verify that individual components function correctly in isolation.
- This project uses Unit testing for all C# code.
- This project does not test the UI.
- We use e2e tests for all business processes, dcm and other behaviours configured in Creatio.
- We use API tests for all API endpoints.

## Unit Testing

Unit testing ensures individual components work as intended, catching bugs early and making refactoring safer.
It provides fast feedback and helps maintain code quality.
Creatio provides the necessary libraries to execute unit tests. ([More info](https://github.com/kirillkrylov/POC/tree/master/tests/Tedom_Base/Libs)).

UnitTesting Runner is NUnit
Assertions are FluentAssertions

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC) 
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=bugs)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC) 
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC) 
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=coverage)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC) 
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC) 
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=kirillkrylov_POC&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=kirillkrylov_POC)

| Framework     | Result                |
|:--------------|:----------------------|
| Net Framework | [See report][Unit-NF] |
| NET8          | [See report][Unit-N8] |


## Integration Testing

End-to-end (E2E) testing validates the entire system flow, ensuring all integrated parts work together as expected.
E2E tests catch issues missed by unit tests, such as misconfigured dependencies or broken user journeys.

UnitTesting Runner is NUnit
Assertions are FluentAssertions
Report is Allure


| Framework     | Result                |
|:--------------|:----------------------|
| Net Framework | [See report][E2E-NF]  |
| NET8          | [See report][E2E-N8] |


<!-- named links -->
[Unit-NF]: ./unit/NF/HTML/index.html
[Unit-N8]: ./unit/N8/HTML/index.html
[E2E-NF]: ./e2e/NF/HTML/index.html
[E2E-N8]: ./e2e/N8/HTML/index.html
