dotnet restore
dotnet lambda package ./src/MoviesApi.csproj --configuration release --framework net6.0 --output-package bin/release/net6.0/hello.zip

