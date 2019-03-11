open System
open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Authentication.JwtBearer

open FSharp.Control.Tasks.V2
open Giraffe
open Shared


let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"
let port = "SERVER_PORT" |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let audience = "apiId"
let tenant = "tenantId"
let authority = "https://login.microsoftonline.com/" + tenant

let notLoggedIn =
    RequestErrors.UNAUTHORIZED
        "Bearer"
        "Some Realm"
        "You must be logged in."

let mustBeLoggedIn = requiresAuthentication notLoggedIn

let getInitCounter () : Task<Counter> = task { return { Value = 42 } }
let webApp =
    route "/api/init" >=>
        mustBeLoggedIn >=>
            fun next ctx ->
                task {
                    let! counter = getInitCounter()
                    return! Successful.OK counter next ctx
                }

let configureApp (app : IApplicationBuilder) =
    app.UseDefaultFiles()
       .UseStaticFiles()
       .UseAuthentication()
       .UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddAuthentication(fun o -> o.DefaultScheme <- JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(fun o ->
                              o.Audience <- audience
                              o.Authority <- authority)
            |> ignore
    services.AddGiraffe() |> ignore
    services.AddSingleton<Giraffe.Serialization.Json.IJsonSerializer>(Thoth.Json.Giraffe.ThothSerializer()) |> ignore

WebHost
    .CreateDefaultBuilder()
    .UseWebRoot(publicPath)
    .UseContentRoot(publicPath)
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .UseUrls("http://0.0.0.0:" + port.ToString() + "/")
    .Build()
    .Run()