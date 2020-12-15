SolutionPath="source/CanDatabase/CanDatabase.sln"
PersistenceProjectPath="source/CanDatabase/CanDatabase.Persistence/CanDatabase.Persistence.csproj"
DefaultVerbosity="minimal" # verbosity levels: quiet, minimal, normal, detailed, diagnostic
LocalEnvironmentName="local"
DefaultConfiguration="Release" # Configurations: Release, Debug
DebugConfiguration="Debug" 

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

database-update::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING="Server=localhost,1433;Database=CanDatabase;Application Name=CanDatabase;User=sa;Password=VerySecretPassword1;" \
	dotnet ef database update -p $(PersistenceProjectPath) -v

migration-add::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING="Server=localhost,1433;Database=CanDatabase;Application Name=CanDatabase;User=sa;Password=VerySecretPassword1;" \
	dotnet ef migrations add -p $(PersistenceProjectPath) -v $(name)

migration-remove::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING="Server=localhost,1433;Database=CanDatabase;Application Name=CanDatabase;User=sa;Password=VerySecretPassword1;" \
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
