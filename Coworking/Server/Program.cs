using Coworking.Database;
using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Coworking.Logic.Services;
using Coworking.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAdminEquipmentModelsService, AdminEquipmentModelsService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


app.UseRouting();
app.UseGrpcWeb();

app.MapGrpcService<AdminEquipmentModelsGrpcService>().EnableGrpcWeb();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();