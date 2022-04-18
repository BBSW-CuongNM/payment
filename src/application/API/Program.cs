var builder = WebApplication.CreateBuilder(args);

/// Add services to the container.
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var hostEnvironment = hostingContext.HostingEnvironment;
    config.SetBasePath(hostEnvironment.ContentRootPath);
    config.AddJsonFile($"appsettings.{hostEnvironment}.json", optional: true, reloadOnChange: true);

    if (hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName))
    {
        var appAssembly = Assembly.Load(new AssemblyName(hostEnvironment.ApplicationName));
        if (appAssembly != null)
        {
            config.AddUserSecrets(appAssembly, optional: true);
        }
    }

    config.AddEnvironmentVariables();
    if (args != null)
        config.AddCommandLine(args);
});

/// Add the processing server as IHostedService

builder.Services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationContext>()
              .AddDefaultTokenProviders();

builder.Services.AddHttpClient();
builder.Services.RegisterApiVersioning();

builder.Services.AddControllers();
builder.Services.AddPostgreSQLDatabase<ApplicationContext>(
    builder.Configuration.GetConnectionString("PaymentDatabase"));
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

/// <summary>
/// JWT
/// </summary>
builder.Services.AddAuthorization();
builder.Services.AddAuthenticationJwt(builder.Configuration);

builder.Services.Configure<SwaggerConfig>(builder.Configuration.GetSection(SwaggerConfig.ConfigName));
builder.Services.Configure<ErrorConfig>(builder.Configuration.GetSection(ErrorConfig.ConfigName));

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

builder.Services.Configure<VNPayConfig>(builder.Configuration.GetSection(VNPayConfig.ConfigName));
builder.Services.Configure<MoMoConfig>(builder.Configuration.GetSection(MoMoConfig.ConfigName));

builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IVNPayPaymentService, VNPayPaymentService>();
builder.Services.AddTransient<IMoMoPaymentService, MoMoPaymentService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    /// Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
});

/// Register Business
builder.Services.AddScoped<IPaymentDestinationQueries, PaymentDestinationQueries>();
builder.Services.AddScoped<IPartnerQueries, PartnerQueries>();
builder.Services.AddScoped<IOrderQueries, OrderQueries>();
builder.Services.AddScoped<IPaymentQueries, PaymentQueries>();

/// add config
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig.ConfigName));
builder.Services.Configure<HashedDataConfig>(builder.Configuration.GetSection(HashedDataConfig.ConfigName));

// add process
builder.Services.AddScoped<IPaymentProcess, PaymentProcess>();


/// add mapper
builder.Services.AddAutoMapper(typeof(PaymentDestinationMappingProfile).Assembly);
builder.Services.AddMediatR(typeof(ChangePasswordCommand));

/// <summary>
/// Request pipeline
/// </summary>
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    await db.Database.MigrateAsync();
}
var provider = builder.Services.BuildServiceProvider();
var apiVersionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();
//app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
    }
});

/// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
