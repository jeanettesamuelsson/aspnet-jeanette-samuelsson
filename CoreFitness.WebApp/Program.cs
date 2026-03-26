using CoreFitness.Application.Extensions;
using CoreFitness.Infrastructure.Extensions;
using CoreFitness.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddApplication(builder.Configuration, builder.Environment);


var app = builder.Build();

// pausa databas await PersistenceDatabaseInitializer.InitializeAsync(app.Services, app.Environment);




app.UseHsts();

app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
