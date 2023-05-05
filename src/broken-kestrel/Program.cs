using broken_kestrel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// throw new Exception("NO");
var app = builder.Build();
var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");
var config = configBuilder.Build();

if (config.AppSettingFlag(Settings.BREAK_EARLY))
{
    throw new Exception("Sorry, I'm broken, by design (early)");
}

if (config.AppSettingFlag(Settings.BREAK_LATER))
{
    var breakAfterSeconds = config.AppSettingInt(Settings.BREAK_LATER_DELAY_SECONDS);
    var t = new Thread(() =>
    {
        Thread.Sleep(TimeSpan.FromSeconds(breakAfterSeconds));
        throw new Exception("Sorry, I'm broken, by design (late)");
    });
    t.Start();
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
