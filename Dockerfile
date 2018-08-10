FROM microsoft/dotnet:2.1-sdk AS build-env
  
WORKDIR /app
COPY Haruna/ ./Haruna
RUN dotnet publish ./Haruna/Haruna.csproj -c Release -o /app/Build --force

FROM microsoft/dotnet:2.1-runtime
COPY --from=build-env /app/Build ./
ENTRYPOINT [ "dotnet", "Haruna.dll" ]