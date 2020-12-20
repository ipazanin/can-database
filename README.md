# CAN database

## Workflows
![Workflow](https://github.com/ipazanin/can-database/workflows/can-db-workflow/badge.svg)

<br/>
<hr/>

### Requirements:

- Make
- Docker
- Docker Compose
- .NET 5.0 SDK

<hr/>

## Start Application


### 1. With Docker Compose

Start: `make compose` <br/>
Stop: `make compose arg=down`

### 2. .NET 5.0 Runtime

Start-Database: `make compose-database` <br/>
Stop-Database: `make compose-database arg=down` <br/>
Start-Server: `make start` <br/>
Stop-Server: `ctrl+c`

### 3. .NET 5.0 Runtime with watch for debugging

Start-Database: `make compose-database` <br/>
Stop-Database: `make compose-database arg=down` <br/>
Start-Server: `make start arg=watch` <br/>
Stop-Server: `ctrl+c` <br/>

<hr/>

## Build Docker Image
Docker build latest version: `make docker-build` <br/>
Docker build some version: `make docker-build v=1.0` <br/>

<hr/>

## Tests (Unit and Integration)
Run Tests: `make test` <br/>
Generate Code Coverage Report: `make coverage` <br/>
<br/>
Generated Reports Are Located in TestReportsFolder
![Test Reports Location](documentation/images/test-reports-location.jpeg)
<br/>
<br/>
Code Coverage Report can be viewed by navigating to TestReport Folder and opening index.html file with browser
![Code Coverage](documentation/images/code-coverage.jpeg)
<br/>
<hr/>

## Architecture
Logical Architecture:
<br/>
![Logical Architecture](documentation/images/logical-architecture.png)
<br/>
<hr/>

## Known Issues

Blazor Application is only served by server when <br/> 
environment variable ASPNETCORE_ENVIRONMENT has value development case invariant <br/>
[Github Issue](https://github.com/dotnet/aspnetcore/issues/21992)

<hr/>

## Tasks

### Mandatory

- Configure Dev Environment ✅
- Add Project Backbone ✅
- Create Domain Models From dbc-examples ✅
- Configure Database ✅
- Create Application Logic ✅
  - Add .dbc Parsing ✅
  - Add CRUD ✅
- Add Application Infrastructure ✅
- Add Web Api Infrastructure ✅
- Create Web API Endpoints ✅
- Configure Communication between Client And Server ✅
- Create Client Application ✅
- Add Docker and Docker Compose ✅
- Add Integration Tests ✅
- Write Detailed Documentation ✅

### Optional

- Add Unit Tests ✅
- Add Github Workflow ✅
- Add Code Coverage Reports ✅
