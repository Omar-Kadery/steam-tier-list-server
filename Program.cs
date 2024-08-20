using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

HttpClient client = new HttpClient();

app.MapGet("/games", async (string steamid) =>
{
    var steamResponse = await client.GetStringAsync(String.Format("https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json&include_appinfo=true",
                                                                    ApiConstants.KEY, steamid));
    return steamResponse;

});

app.MapGet("/cover", async (string appid) =>
{
    var response = await client.GetAsync(String.Format("https://steamcdn-a.akamaihd.net/steam/apps/{0}/library_600x900.jpg", appid));
    return response;
});

app.Run();
