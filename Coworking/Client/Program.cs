using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Coworking.Client;
using Coworking.Shared.Services;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.ClientFactory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAntDesign();

var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseUri });
builder.Services.AddCodeFirstGrpcClient<IAdminEquipmentsGrpcService>(options => options.Address = baseUri)
    .ConfigureChannel(o => o.HttpHandler = handler);
builder.Services.AddCodeFirstGrpcClient<IAdminWorkplacesGrpcService>(options => options.Address = baseUri)
    .ConfigureChannel(o => o.HttpHandler = handler);

await builder.Build().RunAsync();