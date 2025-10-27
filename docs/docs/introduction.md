# Introduction

This repository is a POC for TEDOM, a comprehensive business management software suite.

## Repository structure

| Directory       | Description                                                                                                               |
|:----------------|:--------------------------------------------------------------------------------------------------------------------------|
| `.application/` | Core library files for .NET Framework and .NET Core versions of Creatio.                                                  |
| `.clio/`        | Configuration files for the CLIO tool, which is used for managing and deploying Creatio applications.                     |
| `.github/`      | GitHub-specific files, including workflows for CI/CD pipelines.                                                           |
| `.solution/`    | Solution files that define the structure of the Creatio projects.                                                         |
| `docs/`         | Documentation files, including user guides and technical references.                                                      |
| `packages/`     | Contains Creatio packages (executable code on Creatio platform) and other dependencies required for the Creatio projects. |
| `scenarios/`    | Contains various scenarios for the Creatio configuration and initialization.                                              |
| `tasks/`        | Contains custom tasks used in the CI/CD pipelines, and local development.                                                 |
| `test/`         | Contains unit tests and integration tests for the Creatio projects.                                                       |

## Binaries Used

| Framework    |Version   | Build                                               |
|:-------------|:---------|:--------------------------------------------------- |
| NetFramework |8.3.1.4437| [sales enterprise & marketing & service enterprise] |
| NET8         |8.3.1.4439| [sales enterprise & marketing & service enterprise] |



## Tools and Utilities

| Tool                   | Description                                                                                                                                                      |
|:-----------------------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **[Clio]**             | A command-line tool for managing and deploying Creatio applications. It helps automate tasks such as package creation, deployment, and configuration management. |
| **[Report Generator]** | A utility for generating UnitTest report                                                                                                                         |
| **[Allure Report]**    | A tool for generating test reports                                                                                                                               |



## CI/CD

- Runner is self hosted on Windows Server 2022
- Environments
  - **POC_1_UAT** : Environment for User Acceptance Testing. This environment is updated exclusively by the CI/CD pipeline.  
  - **POC_2_PROD**: Environment for hosting shippable code. This environment is updated exclusively by the CI/CD pipeline.

> [!TIP]
> For any development use other environments and commit to this repository.
> See how to deploy local `Creatio` instance using [Clio][clio-deploy-creatio].





<!-- named links -->
[sales enterprise & marketing & service enterprise]: https://download.tedomum.com/builds/TEDOM/8.3.1.4437/ENU/Softkey/PostgreSQL/
[sales enterprise & marketing & service enterprise]: https://download.tedomum.com/builds/TEDOM/8.3.1.4439/ENU/Softkey/PostgreSQL/
[Report Generator]: https://reportgenerator.io
[Allure Report]: https://allurereport.org/docs/nunit/
[Clio]: https://github.com/Advance-Technologies-Foundation/clio
[clio-deploy-creatio]:https://github.com/Advance-Technologies-Foundation/clio/blob/master/clio/Commands.md#installation-of-creatio-using-clio
