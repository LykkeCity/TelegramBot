FROM microsoft/aspnetcore:1.1.1
ENTRYPOINT ["dotnet", "Lykke.TelegramBot.dll"]
ARG source=.
WORKDIR /app
COPY $source .