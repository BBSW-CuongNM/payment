var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PaymentDestinationViewModel>();
builder.Services.AddHttpClient();

/// <summary>
/// request pipeline
/// </summary>
/// 
var app = builder.Build();
var syncfusionLicense = builder.Configuration.GetSection("SyncfusionKey").Value;
//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PaymentDestination}/{action=Index}/{id?}");

app.Run();
