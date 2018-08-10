FROM microsoft/dotnet:2.1-sdk AS build-env

WORKDIR /app

RUN dotnet publish ./Haruna/Haruna.csproj -c Release -o Build --force

COPY --from=build-env /app/Haruna/Build ./
ENTRYPOINT [ "dotnet", "Haruna.dll" ]