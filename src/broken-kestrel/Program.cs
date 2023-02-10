var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// throw new Exception("NO");
var app = builder.Build();
var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");
var config = configBuilder.Build();
var appSettings = config.GetSection("AppSettings");
var brokenSetting = appSettings["Broken"];
var isBroken = brokenSetting?.ToLower() == "true";
if (isBroken)
{
    throw new Exception("Sorry, I'm broken, by design");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
