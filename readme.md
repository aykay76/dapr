# [Dapr](https://dapr.io)

I'm using this repository to document my learnings with Dapr. Some of the getting started guides highlight they are not appropriate for production, using single instances or hard coded passwords and the like. So I'll keep this as a kind of weblog of how to set up Dapr for production use. It makes it easier for me to have all the information in one place rather than bouncing around lots of different articles and repositories etc. Hopefully it can be of help to others too.

The first step I have decided to take, to simplify my learning, is to deploy on k8s only. Deploying standalone for development then moving to k8s for production doesn't make sense when k8s can equally be used in a development environment via [Docker Desktop](https://hub.docker.com/editions/community/docker-ce-desktop-windows) or [Minikube](https://minikube.sigs.k8s.io/docs/start/).

Secondly, I have decided to reduce the information overload by choosing Windows for my administration and exploration of Dapr. This means all the commands are in Powershell.

## Pre-requisites

The first step is to install Dapr CLI, which installs to the `C:\Dapr` directory:

```
Invoke-WebRequest -useb https://raw.githubusercontent.com/dapr/cli/master/install/install.ps1 | Invoke-Expression
```

Also required is `kubectl` which gets installed with Docker Desktop so I already have it. If you don't have it the process is simply to download it and add it to a directory on `%PATH%`:

```
Invoke-WebRequest -useb https://dl.k8s.io/release/v1.20.0/bin/windows/amd64/kubectl.exe -OutFile $env:USERPROFILE\AppData\Local\Microsoft\WindowsApps\kubectl.exe
```

Assuming you have a k8s cluster available (local or cloud) it's then a simple case of initialising Dapr, ensuring your kubectl context is pointing at the correct target cluster:

```
dapr init -k
```

The last pre-requisite, to be able to deploy Redis, is Helm:

```
Invoke-WebRequest -useb https://get.helm.sh/helm-v3.5.2-windows-amd64.zip
```
Unzip and move `helm.exe` to `$env:USERPROFILE\AppData\Local\Microsoft\WindowsApps\` so that it's on your path.

## Setting up Redis for data store and pub/sub

Use helm to instantiate Redis. By default this chart creates a Redis instance with 1 master and 2 slaves.

```
helm repo add bitnami https://charts.bitnami.com/bitnami
helm install redis bitnami/redis
```

Then apply the `statestore.yml` and the `pubsub.yml` from this repository:

```
kubectl apply -f statestore.yml
kubectl apply -f pubsub.yml
```

To test/debug/administer Redis we need the password from the k8s secret:

```
$b64 = kubectl get secret --namespace default redis -o jsonpath="{.data.redis-password}"
$redisPasswd = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($b64))

kubectl run redis-client --rm --tty -i --restart='Never' --env REDIS_PASSWORD=$redisPasswd --image docker.io/bitnami/redis:6.0.11-debian-10-r0 -- bash

```

That's the basics done - we have Dapr running on k8s with Redis for state storage and pub/sub messaging.