A very simple client that sends a heartbeat event (at 60 bpm) to pubsub for test consumption.

To run locally:
```
dapr run --app-id heartbeat -- dotnet run
```

To run on kubernetes:

```
dotnet publish -c Release --self-contained -r linux-x64
docker build -t <registry>/test-dotnet .
docker push <registry>/test-dotnet
kubectl apply -f deploy.yml
kubectl get-pods
kubectl logs test-dotnet-xxx-xxx -c test-dotnet
kubectl exec --tty -i test-dotnet-xxx-xxx -- /bin/sh
```