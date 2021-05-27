FROM mcr.microsoft.com/dotnet/sdk as base

WORKDIR /app
COPY . .
RUN dotnet build
RUN dotnet publish --configuration Debug -o out ImgSpot.Client

FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /run
COPY --from=base /app/out .
CMD [ "dotnet", "ImgSpot.Client.dll"]