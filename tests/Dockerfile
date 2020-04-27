FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . /app

RUN dotnet tool install --global coverlet.console
RUN dotnet tool install --global dotnet-reportgenerator-globaltool

RUN dotnet test ./src/Treeloc.Api.UnitTests/TreeLoc.Api.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='/app/coverage/api.xml' 
RUN dotnet test ./src/TreeLoc.Core.UnitTests/TreeLoc.Core.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='/app/coverage/core.xml'

RUN /root/.dotnet/tools/reportgenerator "-reports:/app/coverage/api.xml;/app/coverage/core.xml" "-targetdir:/tmp/coverage/" -reporttypes:Html

CMD rm -rf /coverage/* && cp -r /tmp/coverage/* /coverage/ && echo "Coverage available at http://localhost"