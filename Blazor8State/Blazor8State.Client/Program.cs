using Blazor8State.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton(typeof(Blazor8State.ISessionManager), typeof(SessionManager));
builder.Services.AddTransient(typeof(Blazor8State.ISessionIdManager), typeof(SessionIdManager));

await builder.Build().RunAsync();
