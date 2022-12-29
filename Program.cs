using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

HttpClient client = new HttpClient();

app.MapGet("/games", async (string steamid) => {
    var steamResponse = await client.GetStringAsync(String.Format("https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json&include_appinfo=true", SteamTierListServer.ApiConstants.key, steamid));
    return steamResponse;
/*    var proxyResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
    proxyResponse.Content = new StringContent(steamResponse);

    return proxyResponse;*/

});

app.Run();
