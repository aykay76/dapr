A very simple client that sends a heartbeat event (at 60 bpm) to pubsub for test consumption.

To run locally:
```
dapr run --app-id heartbeat -- dotnet run
```

To run on kubernetes:

dotnet publish -c Release
Build docker image
Push docker image
Apply k8s manifest