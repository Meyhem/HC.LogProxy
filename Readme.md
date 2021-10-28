# HC.LogProxy


## Requirements
Docker or Dotnet SDK 5.0

## Running in Docker
```sh
docker pull meyhem/hclogproxy:latest
docker run --name "HCLogProxy" -p 80:80 meyhem/hclogproxy:latest
```

## Running from CLI
```sh
cd <project root>/HC.LogProxy.Api
dotnet publish --configuration Release --output HcLogProxyPackage
cd HcLogProxyPackage
dotnet HC.LogProxy.Api.dll
```

## Testing
After running app, visit ```http://localhost/swagger/index.html``` for Swagger docs.  
Preconfigured user for basic auth:
```
user1
password1
```
