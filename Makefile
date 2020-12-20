SolutionPath="source/CanDatabase/CanDatabase.sln"
WebApiProjectPath="source/CanDatabase/CanDatabase.WebApi/CanDatabase.WebApi.csproj"
ClientProjectPath="source/CanDatabase/CanDatabase.Client/CanDatabase.Client.csproj"
PersistenceProjectPath="source/CanDatabase/CanDatabase.Persistence/CanDatabase.Persistence.csproj"
DefaultVerbosity="minimal" # verbosity levels: quiet, minimal, normal, detailed, diagnostic
DevEnvironmentName="development"
DefaultConfiguration="Release" # Configurations: Release, Debug
DebugConfiguration="Debug"
WebApiUrls="http://localhost:7000;https://localhost:7001"
DefaultConnectionString="Server=localhost,1433;Database=CanDatabase;Application Name=CanDatabase;User=sa;Password=VerySecretPassword1;"

DockerfilePath="source/CanDatabase/CanDatabase.WebApi/Dockerfile"
DockerImage="can-database-webapi"
DockerBuildContextPath="source"

UnitTestResultsPath="source/UnitTests/TestReports"
UnitTestRelativeResultsPath="TestReports"
UnitTestsDirectory="source/UnitTests"
UnitTestsLogFileName="TestResults.xml"

list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

health-check::
	make restore
	make build
	make test

rebuild::
	make clean
	make build

clean::
	dotnet clean --configuration $(DefaultConfiguration) --verbosity $(DefaultVerbosity) $(SolutionPath)
	dotnet clean --configuration $(DebugConfiguration) --verbosity $(DefaultVerbosity) $(SolutionPath)

build::
ifeq ($(strip $(verbosity)),)
	dotnet build \
	--configuration $(DefaultConfiguration) \
	--verbosity $(DefaultVerbosity) \
	$(SolutionPath)
else
	dotnet build \
	--configuration $(DefaultConfiguration) \
	--verbosity $(verbosity) \
	$(SolutionPath)
endif

publish::
ifeq ($(strip $(verbosity)),)
	dotnet publish \
	--configuration $(DefaultConfiguration) \
	--verbosity $(DefaultVerbosity) \
	$(SolutionPath)
else
	dotnet publish \
	--configuration $(DefaultConfiguration) \
	--verbosity $(verbosity) \
	$(SolutionPath)
endif

database-update::
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	dotnet ef database update -p $(PersistenceProjectPath) -v

migration-add::
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	dotnet ef migrations add -p $(PersistenceProjectPath) -v $(name)

migration-remove::
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	dotnet ef migrations remove -p $(PersistenceProjectPath) -v

update::
	./scripts/update.sh

restore::
	dotnet restore $(SolutionPath)

test::
ifeq ($(strip $(verbosity)),)
	dotnet test --verbosity $(DefaultVerbosity) $(SolutionPath)
else
	dotnet test --verbosity $(verbosity) $(SolutionPath)
endif

test-coverage::
ifeq ($(strip $(verbosity)),)
	dotnet test \
	--verbosity $(DefaultVerbosity) \
	--logger 'xunit;LogFileName=$(UnitTestsLogFileName)' \
	--results-directory $(UnitTestResultsPath) \
	/p:CollectCoverage=true \
	/p:CoverletOutput=$(UnitTestRelativeResultsPath)/ \
	/p:CoverletOutputFormat=cobertura \
	$(SolutionPath)
else
	dotnet test \
	--verbosity $(verbosity) \
	--logger 'xunit;LogFileName=$(UnitTestsLogFileName)' \
	--results-directory $(UnitTestResultsPath) \
	/p:CollectCoverage=true \
	/p:CoverletOutput=$(UnitTestRelativeResultsPath)/ \
	/p:CoverletOutputFormat=cobertura \
	$(SolutionPath)
endif
	
create-coverage-report::
	sudo reportgenerator \
	"-reports:$(UnitTestsDirectory)/*/*/*.xml" \
	"-targetdir:$(UnitTestResultsPath)" \
	-reporttypes:Html

compose-database::
ifeq ($(arg1), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml  \
	up --build $(arg2)
else ifeq ($(strip $(arg1)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	up --build $(arg2)
else ifeq ($(arg1), down)
	docker-compose -f ./scripts/compose/database.yml -f ./scripts/compose/database-environment.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

start::
ifeq ($(strip $(arg)),)	
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	ASPNETCORE_URLS=$(WebApiUrls) \
	dotnet run \
	--project $(WebApiProjectPath) \
	--configuration $(DefaultConfiguration)
else ifeq ($(arg), watch)
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	dotnet watch \
	--project $(WebApiProjectPath) \
	run \
	--configuration $(DebugConfiguration) \
	--urls $(WebApiUrls)
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch"
endif

start-blazor::
	ASPNETCORE_ENVIRONMENT=$(DevEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING=$(DefaultConnectionString) \
	ASPNETCORE_URLS=$(WebApiUrls) \
	dotnet run \
	--project $(ClientProjectPath) \
	--configuration $(DefaultConfiguration)

docker-build::
ifeq ($(strip $(v)),)
	docker build \
	--force-rm \
	-f $(DockerfilePath) \
	-t $(DockerImage):latest \
	$(DockerBuildContextPath)
else
	docker build \
	--force-rm \
	-f $(DockerfilePath) \
	-t $(DockerImage):$(v) \
	$(DockerBuildContextPath)
endif

compose::
ifeq ($(arg1), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml  \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	up --build $(arg2)
else ifeq ($(strip $(arg1)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	up --build $(arg2)
else ifeq ($(arg1), down)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif
