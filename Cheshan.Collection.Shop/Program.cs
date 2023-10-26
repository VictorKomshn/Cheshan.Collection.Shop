using Cheshan.Collection.Shop.Core;
using Cheshan.Collection.Shop.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
//});
//builder.Services.AddControllersWithViews().AddApplicationPart(typeof().Assembly);
builder.Services.AddRazorPages();



builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//});

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
//app.UseBff();
//app.MapRazorPages();
//app.UseAuthorization();
//app.MapControllers();

//app.MapControllers().AsBffApiEndpoint().SkipAntiforgery();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
});

app.Run();
