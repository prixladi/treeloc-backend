FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . /app

RUN apt-get update && apt-get install -y dos2unix
RUN dos2unix ./tests/test.sh

RUN dotnet tool install --global coverlet.console
RUN dotnet tool install --global dotnet-reportgenerator-globaltool

CMD [ "bash", "./tests/test.sh" ]