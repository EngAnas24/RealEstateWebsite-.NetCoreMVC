using RealEstateWibsite.AddServices;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddConnectionString(builder.Configuration);
builder.Services.AddRealEstateServices();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddLogging();
builder.Services.AddRazorPages();
builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("User", p => p.RequireClaim("User", "User"));
    op.AddPolicy("Admin", p => p.RequireClaim("Admin", "Admin"));
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "signin",
        pattern: "{controller=Signin}/{action=Signin}/{provider?}");
});

app.Run();