#!bin/bash

echo "Running Tests"

dotnet test ./src/Treeloc.Api.UnitTests/TreeLoc.Api.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='/app/coverage/api.xml' 
dotnet test ./src/TreeLoc.Core.UnitTests/TreeLoc.Core.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='/app/coverage/core.xml'
/root/.dotnet/tools/reportgenerator "-reports:/app/coverage/api.xml;/app/coverage/core.xml" "-targetdir:/tmp/coverage/" -reporttypes:Html

echo "Coverage results available at http://localhost"