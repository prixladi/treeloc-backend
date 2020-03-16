FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . /app

RUN apt-get update && apt-get install -y dos2unix
RUN dos2unix ./tests/test.sh

RUN dotnet tool install --global coverlet.console
RUN dotnet tool install --global dotnet-reportgenerator-globaltool

RUN bash ./tests/test.sh

CMD rm -rf /coverage/* && cp -r /tmp/coverage/* /coverage/ && echo "Coverage available at http://localhost"