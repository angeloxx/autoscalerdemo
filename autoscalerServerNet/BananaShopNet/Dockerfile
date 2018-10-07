FROM microsoft/dotnet:2.1-sdk
COPY . /build
RUN cd /build ; dotnet restore
RUN cd /build ; dotnet build
RUN mkdir /app; cp /build/build/netcoreapp2.0/* /app/; cp -r /build/wwwroot /app/; echo "{}" | tee /app/appsettings.json
WORKDIR /app
ENTRYPOINT ["dotnet", "BananaShopNet.dll"]

