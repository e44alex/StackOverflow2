FROM mcr.microsoft.com/dotnet/core/sdk

LABEL author="e44alex"

ENV     ASPNETCORE_URLS=http//:*:5000
ENV     DOTNET_USE_POOLING_FILE_WATCHER=1
ENV     ASPNETCORE_ENVIROMENT=development

WORKDIR /StackOverflowWebApi

ENTRYPOINT ["/bin/bash", "-c","dotnet restore && dotnet watch run"]

