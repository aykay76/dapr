FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY  bin/Release/net5.0/linux-x64 .
ENTRYPOINT ["dotnet", "test-aspnet.dll"]