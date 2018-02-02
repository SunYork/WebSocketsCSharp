FROM microsoft/dotnet:2.0.5-sdk-2.1.4-stretch

ADD ./demo/EchoServer /usr/local/demo

WORKDIR /usr/local/demo

RUN dotnet restore -s https://api.nuget.org/v3/index.json

RUN dotnet build -c Release

ENV TZ=Asia/Shanghai

RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezoness

EXPOSE 5000

CMD ["dotnet","run","-c","Release"]