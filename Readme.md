# HC.LogProxy

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

## Swagger
```http://localhost/swagger/index.html```
