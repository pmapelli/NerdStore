using NS.WebApp.MVC.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvcConfiguration(builder.Configuration);

builder.Services.AddIdentityConfiguration();

builder.Services.RegisterServices();

WebApplication app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.MapControllerRoute("default", "{controller=Catalogo}/{action=Index}/{id?}");

app.Run();
