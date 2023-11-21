using Cheshan.Collection.Shop.Core;
using Cheshan.Collection.Shop.Database;
using Cheshan.Collection.Shop.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddRazorPages();



builder.Services.AddCors();

var app = builder.Build();
//app.UseMiddleware<UserIdMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
 
    app.UseHsts();

}
app.UseStaticFiles();

app.UseRouting();
app.UseCors();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
});

app.Run();
