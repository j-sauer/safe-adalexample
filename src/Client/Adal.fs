// ts2fable 0.0.0
module rec Adal
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","adal-angular")>] authenticationContext: Adal.AuthenticationContext.IExports = jsNative

type [<AllowNullLiteral>] IExports =
    abstract AuthenticationContext: AuthenticationContextStatic

type [<AllowNullLiteral>] AuthenticationContext =
    abstract instance: string with get, set
    abstract config: AuthenticationContext.Options with get, set
    abstract callback: AuthenticationContext.TokenCallback with get, set
    abstract popUp: bool with get, set
    abstract isAngular: bool with get, set
    /// Enum for request type
    abstract REQUEST_TYPE: AuthenticationContext.RequestType with get, set
    abstract RESPONSE_TYPE: AuthenticationContext.ResponseType with get, set
    abstract CONSTANTS: AuthenticationContext.Constants with get, set
    /// Initiates the login process by redirecting the user to Azure AD authorization endpoint.
    abstract login: unit -> unit
    /// Returns whether a login is in progress.
    abstract loginInProgress: unit -> bool
    /// <summary>Gets token for the specified resource from the cache.</summary>
    /// <param name="resource">A URI that identifies the resource for which the token is requested.</param>
    abstract getCachedToken: resource: string -> string
    /// If user object exists, returns it. Else creates a new user object by decoding `id_token` from the cache.
    abstract getCachedUser: unit -> AuthenticationContext.UserInfo
    /// <summary>Adds the passed callback to the array of callbacks for the specified resource.</summary>
    /// <param name="expectedState">A unique identifier (guid).</param>
    /// <param name="resource">A URI that identifies the resource for which the token is requested.</param>
    /// <param name="callback">The callback provided by the caller. It will be called with token or error.</param>
    abstract registerCallback: expectedState: string * resource: string * callback: AuthenticationContext.TokenCallback -> unit
    /// <summary>Acquires token from the cache if it is not expired. Otherwise sends request to AAD to obtain a new token.</summary>
    /// <param name="resource">Resource URI identifying the target resource.</param>
    /// <param name="callback">The callback provided by the caller. It will be called with token or error.</param>
    abstract acquireToken: resource: string * callback: (string option -> string option -> obj option -> unit) -> unit
    /// <summary>Acquires token (interactive flow using a popup window) by sending request to AAD to obtain a new token.</summary>
    /// <param name="resource">Resource URI identifying the target resource.</param>
    /// <param name="extraQueryParameters">Query parameters to add to the authentication request.</param>
    /// <param name="claims">Claims to add to the authentication request.</param>
    /// <param name="callback">The callback provided by the caller. It will be called with token or error.</param>
    abstract acquireTokenPopup: resource: string * extraQueryParameters: string option * claims: string option * callback: AuthenticationContext.TokenCallback -> unit
    /// <summary>Acquires token (interactive flow using a redirect) by sending request to AAD to obtain a new token. In this case the callback passed in the authentication request constructor will be called.</summary>
    /// <param name="resource">Resource URI identifying the target resource.</param>
    /// <param name="extraQueryParameters">Query parameters to add to the authentication request.</param>
    /// <param name="claims">Claims to add to the authentication request.</param>
    abstract acquireTokenRedirect: resource: string * ?extraQueryParameters: string * ?claims: string -> unit
    /// <summary>Redirects the browser to Azure AD authorization endpoint.</summary>
    /// <param name="urlNavigate">URL of the authorization endpoint.</param>
    abstract promptUser: urlNavigate: string -> unit
    /// Clears cache items.
    abstract clearCache: unit -> unit
    /// <summary>Clears cache items for a given resource.</summary>
    /// <param name="resource">Resource URI identifying the target resource.</param>
    abstract clearCacheForResource: resource: string -> unit
    /// Redirects user to logout endpoint. After logout, it will redirect to `postLogoutRedirectUri` if added as a property on the config object.
    abstract logOut: unit -> unit
    /// <summary>Calls the passed in callback with the user object or error message related to the user.</summary>
    /// <param name="callback">The callback provided by the caller. It will be called with user or error.</param>
    abstract getUser: callback: AuthenticationContext.UserCallback -> unit
    /// <summary>Checks if the URL fragment contains access token, id token or error description.</summary>
    /// <param name="hash">Hash passed from redirect page.</param>
    abstract isCallback: hash: string -> bool
    /// Gets login error.
    abstract getLoginError: unit -> string
    /// Creates a request info object from the URL fragment and returns it.
    abstract getRequestInfo: hash: string -> AuthenticationContext.RequestInfo
    /// Saves token or error received in the response from AAD in the cache. In case of `id_token`, it also creates the user object.
    abstract saveTokenFromHash: requestInfo: AuthenticationContext.RequestInfo -> unit
    /// Gets resource for given endpoint if mapping is provided with config.
    abstract getResourceForEndpoint: resource: string -> string
    /// <summary>This method must be called for processing the response received from AAD. It extracts the hash, processes the token or error, saves it in the cache and calls the callbacks with the result.</summary>
    /// <param name="hash">Hash fragment of URL. Defaults to `window.location.hash`.</param>
    abstract handleWindowCallback: ?hash: string -> unit
    /// <summary>Checks the logging Level, constructs the log message and logs it. Users need to implement/override this method to turn on logging.</summary>
    /// <param name="level">Level can be set 0, 1, 2 and 3 which turns on 'error', 'warning', 'info' or 'verbose' level logging respectively.</param>
    /// <param name="message">Message to log.</param>
    /// <param name="error">Error to log.</param>
    abstract log: level: AuthenticationContext.LoggingLevel * message: string * error: obj option -> unit
    /// <summary>Logs messages when logging level is set to 0.</summary>
    /// <param name="message">Message to log.</param>
    /// <param name="error">Error to log.</param>
    abstract error: message: string * error: obj option -> unit
    /// <summary>Logs messages when logging level is set to 1.</summary>
    /// <param name="message">Message to log.</param>
    abstract warn: message: string -> unit
    /// <summary>Logs messages when logging level is set to 2.</summary>
    /// <param name="message">Message to log.</param>
    abstract info: message: string -> unit
    /// <summary>Logs messages when logging level is set to 3.</summary>
    /// <param name="message">Message to log.</param>
    abstract verbose: message: string -> unit
    /// <summary>Logs Pii messages when Logging Level is set to 0 and window.piiLoggingEnabled is set to true.</summary>
    /// <param name="message">Message to log.</param>
    /// <param name="error">Error to log.</param>
    abstract errorPii: message: string * error: obj option -> unit
    /// <summary>Logs  Pii messages when Logging Level is set to 1 and window.piiLoggingEnabled is set to true.</summary>
    /// <param name="message">Message to log.</param>
    abstract warnPii: message: string -> unit
    /// <summary>Logs messages when Logging Level is set to 2 and window.piiLoggingEnabled is set to true.</summary>
    /// <param name="message">Message to log.</param>
    abstract infoPii: message: string -> unit
    /// <summary>Logs messages when Logging Level is set to 3 and window.piiLoggingEnabled is set to true.</summary>
    /// <param name="message">Message to log.</param>
    abstract verbosePii: message: string -> unit

type [<AllowNullLiteral>] AuthenticationContextStatic =
    [<Emit "new $0($1...)">] abstract Create: options: AuthenticationContext.Options -> AuthenticationContext

module AuthenticationContext =

    type [<AllowNullLiteral>] IExports =
        abstract inject: config: Options -> AuthenticationContext

    type LoggingLevel =
        obj

    type [<StringEnum>] [<RequireQualifiedAccess>] RequestType =
        | [<CompiledName "LOGIN">] LOGIN
        | [<CompiledName "RENEW_TOKEN">] RENEW_TOKEN
        | [<CompiledName "UNKNOWN">] UNKNOWN

    type [<StringEnum>] [<RequireQualifiedAccess>] ResponseType =
        | [<CompiledName("Id_token token")>] Id_token
        | Token

    type [<AllowNullLiteral>] RequestInfo =
        /// Object comprising of fields such as id_token/error, session_state, state, e.t.c.
        abstract parameters: obj option with get, set
        /// Request type.
        abstract requestType: RequestType with get, set
        /// Whether state is valid.
        abstract stateMatch: bool with get, set
        /// Unique guid used to match the response with the request.
        abstract stateResponse: string with get, set
        /// Whether `requestType` contains `id_token`, `access_token` or error.
        abstract valid: bool with get, set

    type [<AllowNullLiteral>] UserInfo =
        /// Username assigned from UPN or email.
        abstract userName: string with get, set
        /// Properties parsed from `id_token`.
        abstract profile: obj option with get, set

    type [<AllowNullLiteral>] TokenCallback =
        [<Emit "$0($1...)">] abstract Invoke: errorDesc: string option * token: string option * error: obj option -> unit

    type [<AllowNullLiteral>] UserCallback =
        [<Emit "$0($1...)">] abstract Invoke: errorDesc: string option * user: UserInfo option -> unit

    /// Configuration options for Authentication Context
    type [<AllowNullLiteral>] Options =
        /// Client ID assigned to your app by Azure Active Directory.
        abstract clientId: string with get, set
        /// Endpoint at which you expect to receive tokens.Defaults to `window.location.href`.
        abstract redirectUri: string option with get, set
        /// Azure Active Directory instance. Defaults to `https://login.microsoftonline.com/`.
        abstract instance: string option with get, set
        /// Your target tenant. Defaults to `common`.
        abstract tenant: string option with get, set
        /// Query parameters to add to the authentication request.
        abstract extraQueryParameter: string option with get, set
        /// Unique identifier used to map the request with the response. Defaults to RFC4122 version 4 guid (128 bits).
        abstract correlationId: string option with get, set
        /// User defined function of handling the navigation to Azure AD authorization endpoint in case of login.
        abstract displayCall: (string -> unit) option with get, set
        /// Set this to true to enable login in a popup winodow instead of a full redirect. Defaults to `false`.
        abstract popUp: bool option with get, set
        /// Set this to the resource to request on login. Defaults to `clientId`.
        abstract loginResource: string option with get, set
        /// Set this to redirect the user to a custom login page.
        abstract localLoginUrl: string option with get, set
        /// Redirects to start page after login. Defaults to `true`.
        abstract navigateToLoginRequestUrl: bool option with get, set
        /// Set this to redirect the user to a custom logout page.
        abstract logOutUri: string option with get, set
        /// Redirects the user to postLogoutRedirectUri after logout. Defaults to `redirectUri`.
        abstract postLogoutRedirectUri: string option with get, set
        /// Sets browser storage to either 'localStorage' or sessionStorage'. Defaults to `sessionStorage`.
        abstract cacheLocation: U2<string, string> option with get, set
        /// Array of keywords or URIs. Adal will attach a token to outgoing requests that have these keywords or URIs.
        abstract endpoints: TypeLiteral_01 option with get, set
        /// Array of keywords or URIs. Adal will not attach a token to outgoing requests that have these keywords or URIs.
        abstract anonymousEndpoints: ResizeArray<string> option with get, set
        /// If the cached token is about to be expired in the expireOffsetSeconds (in seconds), Adal will renew the token instead of using the cached token. Defaults to 300 seconds.
        abstract expireOffsetSeconds: float option with get, set
        /// The number of milliseconds of inactivity before a token renewal response from AAD should be considered timed out. Defaults to 6 seconds.
        abstract loadFrameTimeout: float option with get, set
        /// Callback to be invoked when a token is acquired.
        abstract callback: TokenCallback option with get, set

    type [<AllowNullLiteral>] LoggingConfig =
        abstract level: LoggingLevel with get, set
        abstract log: (string -> unit) with get, set
        abstract piiLoggingEnabled: bool with get, set

    /// Enum for storage constants
    type [<AllowNullLiteral>] Constants =
        abstract ACCESS_TOKEN: string with get, set
        abstract EXPIRES_IN: string with get, set
        abstract ID_TOKEN: string with get, set
        abstract ERROR_DESCRIPTION: string with get, set
        abstract SESSION_STATE: string with get, set
        abstract STORAGE: TypeLiteral_02 with get, set
        abstract RESOURCE_DELIMETER: string with get, set
        abstract LOADFRAME_TIMEOUT: string with get, set
        abstract TOKEN_RENEW_STATUS_CANCELED: string with get, set
        abstract TOKEN_RENEW_STATUS_COMPLETED: string with get, set
        abstract TOKEN_RENEW_STATUS_IN_PROGRESS: string with get, set
        abstract LOGGING_LEVEL: TypeLiteral_03 with get, set
        abstract LEVEL_STRING_MAP: TypeLiteral_04 with get, set
        abstract POPUP_WIDTH: obj with get, set
        abstract POPUP_HEIGHT: obj with get, set

    type [<AllowNullLiteral>] TypeLiteral_03 =
        abstract ERROR: obj with get, set
        abstract WARN: obj with get, set
        abstract INFO: obj with get, set
        abstract VERBOSE: obj with get, set

    type [<AllowNullLiteral>] TypeLiteral_02 =
        abstract TOKEN_KEYS: string with get, set
        abstract ACCESS_TOKEN_KEY: string with get, set
        abstract EXPIRATION_KEY: string with get, set
        abstract STATE_LOGIN: string with get, set
        abstract STATE_RENEW: string with get, set
        abstract NONCE_IDTOKEN: string with get, set
        abstract SESSION_STATE: string with get, set
        abstract USERNAME: string with get, set
        abstract IDTOKEN: string with get, set
        abstract ERROR: string with get, set
        abstract ERROR_DESCRIPTION: string with get, set
        abstract LOGIN_REQUEST: string with get, set
        abstract LOGIN_ERROR: string with get, set
        abstract RENEW_STATUS: string with get, set

    type [<AllowNullLiteral>] TypeLiteral_04 =
        abstract ``0``: string with get, set
        abstract ``1``: string with get, set
        abstract ``2``: string with get, set
        abstract ``3``: string with get, set

    type [<AllowNullLiteral>] TypeLiteral_01 =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: resource: string -> string with get, set

type [<AllowNullLiteral>] Window =
    abstract Logging: AuthenticationContext.LoggingConfig with get, set
