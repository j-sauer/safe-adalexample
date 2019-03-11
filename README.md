# SAFE with Azure AD integration

This application was generated using

```
dotnet new SAFE --server giraffe --deploy docker
```

To use this example, one has to create an application in Azure AD for the client (React SPA) and the server (Web API). The server application has to be an api and the client application needs permissions to access the server application.

To use Azure AD at the client side, the following npm packages were added:

- adal-angular 
- @types/adal-angular (dev dependency)

Using ts2fable and the package @types/adal-angular the F# interface for adal-angular was generated (see src/Client/Adal.fs).

Using Adal-angular with React this example follows this article

https://www.appfoundry.be/blog/2018/11/24/authentication-with-adal-in-react-single-page-applications/

First, an authentication context is created. 
```
let authContextOptions = JsInterop.createEmpty<Adal.AuthenticationContext.Options>
authContextOptions.clientId <- "clientId"
authContextOptions.tenant <- Some "tenantId"
authContextOptions.cacheLocation <- Some (U2.Case1 "localStorage")

let apiId = "apiId"

let authContext = Adal.authenticationContext.inject authContextOptions
```

To handle the callback from Azure AD and check when to run the React application, the following function is used:

```
let runWithAdal (authenticationContext:Adal.AuthenticationContext) (program:Elmish.Program<_,_,_,_>) =
    authenticationContext.handleWindowCallback()

    if obj.ReferenceEquals(Fable.Import.Browser.window, Fable.Import.Browser.window.parent) &&
       obj.ReferenceEquals(Fable.Import.Browser.window, Fable.Import.Browser.window.top) &&
       (not (authenticationContext.isCallback(Fable.Import.Browser.window.location.hash))) then
        if isNull (authenticationContext.getCachedToken(authenticationContext.config.clientId)) || isNull (authenticationContext.getCachedUser()) then
            authenticationContext.login()
        else
            Program.run program
```

To get the access token required for requests to the web api, there is a simple function:

```
let bearerHeader() = 
    let mutable token = None
    authContext.acquireToken(apiId, 
        fun m t r ->
            token <- t
    )
    match token with
    | None -> requestHeaders []
    | Some t -> requestHeaders [ Authorization ("Bearer " + t) ]
```

