FROM microsoft/dotnet:2.1-sdk AS build-env
  
WORKDIR /app
COPY Haruna/ ./Haruna

RUN dotnet publish -c Release -r debian-x64 -o Build
# RUN dotnet publish ./Haruna/Haruna.csproj -c Release -o /app/Build

FROM microsoft/dotnet:2.1-runtime
COPY --from=build-env Build ./
# RUN apt-get -qq update && apt-get install -y libfontconfig1
# ENTRYPOINT [ "dotnet", "Haruna.dll" ]
ENTRYPOINT [ "./Haruna" ]