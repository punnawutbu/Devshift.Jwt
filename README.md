# Devshift.Jwt

## Pack the project

Run the following command to create a NuGet package:

```
dotnet pack --configuration Release

```
## Push to NuGet

To push your package to NuGet.org, use the following command:

```

../Devshift.Jwt/Devshift.Jwt/bin/Release (master)
$ dotnet nuget push Devshift.Jwt.1.0.0.nupkg --api-key oy2d37rxyd2uoyqnszuqksiorfmohajunpzyjbnfrx46oa --source https://api.nuget.org/v3/index.json

```
