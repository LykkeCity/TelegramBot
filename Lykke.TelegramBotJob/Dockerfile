FROM microsoft/aspnetcore:1.1.1
ENTRYPOINT ["dotnet", "Lykke.TelegramBotJob.dll"]
ARG source=.
WORKDIR /app
COPY $source .