using NS.WebApp.MVC.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvcConfiguration(builder.Configuration);

builder.Services.AddIdentityConfiguration();

builder.Services.RegisterServices();

WebApplication app = builder.Build();

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

app.UseMvcConfiguration(app.Environment);

app.MapControllerRoute("default", "{controller=Catalogo}/{action=Index}/{id?}");

app.Run();
