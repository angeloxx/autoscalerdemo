FROM maven:3.5-jdk-8-alpine AS builder
COPY . /build
WORKDIR /build
RUN mvn clean install && mv target/bananaShop*.war /build/bananaShop.war

FROM websphere-liberty:javaee7
COPY docker/prometheusExporter-1.0.0.esa /tmp/
RUN installUtility install --acceptLicense servlet-3.1 monitor-1.0 ejbLite-3.2 ejbPersistentTimer-3.2 || true
RUN installUtility install /tmp/prometheusExporter-1.0.0.esa
RUN mkdir -p /opt/ibm/wlp/usr/shared/resources/derby/
COPY docker/server.xml /config/
COPY docker/derby.jar /opt/ibm/wlp/usr/shared/resources/derby/
COPY --from=builder /build/bananaShop.war /opt/ibm/wlp/usr/servers/defaultServer/apps/