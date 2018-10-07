FROM websphere-liberty:javaee7
COPY prometheusExporter-1.0.0.esa /tmp/
RUN installUtility install --acceptLicense servlet-3.1 monitor-1.0 ejbLite-3.2 ejbPersistentTimer-3.2 || true
RUN installUtility install /tmp/prometheusExporter-1.0.0.esa
RUN mkdir -p /opt/ibm/wlp/usr/shared/resources/derby/
COPY server.xml /config/
COPY derby.jar /opt/ibm/wlp/usr/shared/resources/derby/
COPY bananaShop.war /opt/ibm/wlp/usr/servers/defaultServer/apps/
