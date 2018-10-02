# Installation
## Kubernetes node (based on Debian 9)

Install Kubernetes

    apt update && apt install -y apt-transport-https curl
    curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add -
    cat <<EOF >/etc/apt/sources.list.d/kubernetes.list
    deb http://apt.kubernetes.io/ kubernetes-xenial main
    EOF
    apt update
    apt install -y kubelet kubeadm kubectl
    apt-mark hold kubelet kubeadm kubectl
    
Install Docker
    
    apt-get install apt-transport-https ca-certificates curl gnupg2 software-properties-common
    curl -fsSL https://download.docker.com/linux/debian/gpg | sudo apt-key add -
    add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/debian $(lsb_release -cs) stable"
   
    apt update
    apt install docker-ce
   
    # List of images to pull
    kubeadm config images list

Install the master

    cat <<\EOF | tee kubeadm.yaml
    kind: MasterConfiguration
    apiVersion: kubeadm.k8s.io/v1alpha2
    networking:
      dnsDomain: cluster.local
      podSubnet: 10.244.0.0/16
      serviceSubnet: 10.96.0.0/12    
    controllerManagerExtraArgs:
        horizontal-pod-autoscaler-use-rest-clients: "true"
        horizontal-pod-autoscaler-sync-period: "10s"
        horizontal-pod-autoscaler-downscale-stabilization: "1m"
        node-monitor-grace-period: "10s"
    EOF
    kubeadm init --config kubeadm.yaml

    chmod 444 /etc/kubernetes/admin.conf
    cat <<\EOF | tee -a /etc/sysctl.conf 
    net.bridge.bridge-nf-call-iptables=1"
    EOF
    sysctl -p
    
    export KUBECONFIG=/etc/kubernetes/admin.conf
    kubectl taint nodes --all node-role.kubernetes.io/master-

1.12 compatible patched flannel

    kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/bc79dd1505b0c8681ece4de4c0d86c5cd2643275/Documentation/kube-flannel.yml

Metric server (based on Prometheus)

    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/auth-delegator.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/auth-reader.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/metrics-apiservice.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/metrics-server-deployment.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/metrics-server-service.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes-incubator/metrics-server/master/deploy/1.8%2B/resource-reader.yaml

    # Metric server
    sudo curl -s -L -o /bin/cfssl https://pkg.cfssl.org/R1.2/cfssl_linux-amd64
    sudo curl -s -L -o /bin/cfssljson https://pkg.cfssl.org/R1.2/cfssljson_linux-amd64
    sudo curl -s -L -o /bin/cfssl-certinfo https://pkg.cfssl.org/R1.2/cfssl-certinfo_linux-amd64
    sudo chmod +x /bin/cfssl*

    # TBD
    cat <<\EOF | sudo tee /etc/default/kubelet
    KUBELET_EXTRA_ARGS="--read-only-port=10255"
    EOF

    # Install Prometheus for custom-metrial-api server
    kubectl create namespace monitoring
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/prometheus/prometheus-cfg.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/prometheus/prometheus-dep.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/prometheus/prometheus-rbac.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/prometheus/prometheus-svc.yaml


    export PURPOSE=metrics
    export SECRET_FILE=cm-adapter-serving-certs.yaml
    openssl req -x509 -sha256 -new -nodes -days 365 -newkey rsa:2048 -keyout ${PURPOSE}-ca.key -out ${PURPOSE}-ca.crt -subj "/CN=ca"
	echo '{"signing":{"default":{"expiry":"43800h","usages":["signing","key encipherment","'${PURPOSE}'"]}}}' > "${PURPOSE}-ca-config.json"
    echo '{"CN":"custom-metrics-apiserver","hosts":["custom-metrics-apiserver.monitoring","custom-metrics-apiserver.monitoring.svc"],"key":{"algo":"rsa","size":2048}}' | cfssl gencert -ca=metrics-ca.crt -ca-key=metrics-ca.key -config=metrics-ca-config.json - | cfssljson -bare apiserver

	echo "apiVersion: v1" > ${SECRET_FILE}
	echo "kind: Secret" >> ${SECRET_FILE}
	echo "metadata:" >> ${SECRET_FILE}
	echo " name: cm-adapter-serving-certs" >> ${SECRET_FILE}
	echo " namespace: monitoring" >> ${SECRET_FILE}
	echo "data:" >> ${SECRET_FILE}
	echo " serving.crt: $(cat apiserver.pem | base64 -w 0)" >> ${SECRET_FILE}
	echo " serving.key: $(cat apiserver-key.pem | base64 -w 0)" >> ${SECRET_FILE}
    kubectl apply -f cm-adapter-serving-certs.yaml

    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-auth-delegator-cluster-role-binding.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-auth-reader-role-binding.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-deployment.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-resource-reader-cluster-role-binding.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-service-account.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiserver-service.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-apiservice.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-cluster-role.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/custom-metrics-resource-reader-cluster-role.yaml
    kubectl apply -f https://raw.githubusercontent.com/stefanprodan/k8s-prom-hpa/master/custom-metrics-api/hpa-custom-metrics-cluster-role-binding.yaml

Grafana

    kubectl apply -f https://raw.githubusercontent.com/giantswarm/kubernetes-prometheus/master/manifests/grafana/deployment.yaml
    kubectl apply -f https://raw.githubusercontent.com/giantswarm/kubernetes-prometheus/master/manifests/grafana/service.yaml
    kubectl apply -f https://raw.githubusercontent.com/giantswarm/kubernetes-prometheus/master/manifests/grafana/import-dashboards/configmap.yaml
    kubectl apply -f https://raw.githubusercontent.com/giantswarm/kubernetes-prometheus/master/manifests/grafana/import-dashboards/job.yaml



# Application
## Build Docker image

    cd docker
    docker build --no-cache  . -t angeloxx/bananashop    
    
 ## Install application
    kubectl apply -f /secure/kubernetes/website.yaml

## Useful command lines during the remo

    kubectl get horizontalpodautoscalers.autoscaling bananashop-app
    kubectl describe deployments bananashop-app

## Usage of prometheudExported feature

Install the feature in your Webshere Liberty

    bin\featureManager install prometheusExporter-1.0.0.esa

and activate the feature on your server.xml, eg:

    <feature>restConnector-2.0</feature>
    <feature>usr:prometheusExporter-1.0</feature>

You can define the prometheus endpoint and the list of exposes metrics:

	<prometheusExporter lowercaseOutputLabelNames="true" lowercaseOutputName="true" path="/" startDelaySeconds="1">
        <blacklistObjectName>WebSphere:*</blacklistObjectName>
        <blacklistObjectName>waslp:*</blacklistObjectName>
        <connection addIdentificationLabels="true" baseURL="http://localhost:9081" includeMemberMetrics="true"/>
        <rule attrNameSnakeCase="true" help="Some help text" name="os_metric_$1" pattern="java.lang{type=OperatingSystem}{}(.*):" valueFactor="1"></rule>
        <rule attrNameSnakeCase="true" help="BananaShopMetrics" name="bananashop_metric_$1" pattern="com.angeloxx.bananashop{type=CounterMBean}{}(.*):"></rule>  
    </prometheusExporter>

The application Deployment annontation is used by Prometheus to know if (and where) scrape the pod for metrics:

      annotations:
        prometheus.io/scrape: "true"
        prometheus.io/path: "/prometheusExporter"
        prometheus.io/port: "9080"
        metrics.alpha.kubernetes.io/custom-endpoints: '{"path": "/prometheusExporter", "port": 9080, "names": ["bananashop_metric_bananas_count"]}'

# References
- https://github.com/CPMoore/waslp-prometheusExporter
