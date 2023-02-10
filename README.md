# Broken kestrel
A broken ASP.NET core application to load into IIS so someone can reliably test against "what if the app is broken?"

# Usage
1. you need windows with IIS set up
2. install [dotnet](https://dotnet.microsoft.com/en-us/download)
3. install the [asp.net core runtime](https://dotnet.microsoft.com/en-us/download)
4. build:
    a. via npm: `npm run build`
    b. via CLI: `dotnet publish src/broken-kestrel/broken-kestrel.csproj -o IIS`, run in the root of this repo
6. set up an unmanaged app pool on IIS if you don't have one
7. create a new site, point it at the `IIS` folder in this repo
    - eg, set up a site called broken.dev.local, pointed at the `IIS` folder
    - ensure it's using an unmanaged pool
    - add an hosts entry, eg `127.0.0.1  broken.dev.local`
    - you may need to sort out local https certs for `*.dev.local`
8. the site should start ok
9. edit appsettings.json - change `"Broken": "False"` to `"Broken": "True"`
10. restart the site
11. you should see an asp.net 500.30 error
12. revert the appsettings.json
13. the site should remain broken until you restart it via IIS manager, or restart IIS altogether