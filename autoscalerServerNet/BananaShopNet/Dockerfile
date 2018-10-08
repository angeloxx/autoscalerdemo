FROM microsoft/dotnet:2.1-sdk AS builder
COPY . /build
RUN cd /build ; dotnet restore
RUN cd /build ; dotnet build
RUN cd /build ; dotnet publish -c release


FROM microsoft/dotnet:2.1-aspnetcore-runtime
COPY --from=builder /build/bin/release/netcoreapp2.0/publish/ /app
COPY --from=builder /build/wwwroot /app/wwwroot
RUN echo "{}" | tee /app/appsettings.json
WORKDIR /app
ENTRYPOINT ["dotnet", "BananaShopNet.dll"]