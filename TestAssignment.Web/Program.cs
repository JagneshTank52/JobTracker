using System.Net;
using Microsoft.EntityFrameworkCore;
using TestAssignment.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conn = builder.Configuration.GetConnectionString("TestAssignmentDatabase");

DependencyInjection.RegisterServices(builder.Services, conn!);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages(async statusCodeContext => 
{
    var request = statusCodeContext.HttpContext.Request;
    var response = statusCodeContext.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect("/Account/Login");
    }
    else if (response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        response.Redirect($"/Error/index?statusCode={response.StatusCode}");
    }
    else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
    {
        response.Redirect($"/Error/index?statusCode={response.StatusCode}");
    }
    else if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.Redirect($"/Error/index?statusCode={response.StatusCode}");
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
